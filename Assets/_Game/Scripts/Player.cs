using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform center, center2;
    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask pushLayer;
    [SerializeField] private LayerMask endLayer;
    [SerializeField] private LayerMask winLayer;
    [SerializeField] private LayerMask chestLayer;
    [SerializeField] private LayerMask diamondLayer;
    [SerializeField] private GameObject brickHolder, brickPrefab, PlayerObject;
    [SerializeField] private RunningState currentState;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform chestTrans;
    [SerializeField] private GameObject openChestPrefab;
    public List<GameObject> brickHoldingList;
    private bool isRunning = true;
    private bool isEnding = false;
    private int winCount = 0;
    private bool isWinning = false;
    private bool isCelerbrate = false;

    private int brickCount = 0;

    public enum RunningState{
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(gameObject.transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Physics.Raycast(center.position, Vector3.down, Mathf.Infinity, pushLayer));
        if(Input.GetKeyDown(KeyCode.W)){
            if(CheckRunningState(RunningState.Up)){
                MoveWithState(RunningState.Up);
            }
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            if(CheckRunningState(RunningState.Down)){
                MoveWithState(RunningState.Down);
            }
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            if(CheckRunningState(RunningState.Left)){
                MoveWithState(RunningState.Left);
            }
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            if(CheckRunningState(RunningState.Right)){
                MoveWithState(RunningState.Right);
            }
        }
        CheckGetBrick();
        if(!isEnding){
            CheckOnEnding();
        }
        if(isWinning && chestTrans != null){
            transform.position = Vector3.MoveTowards(transform.position, chestTrans.position - new Vector3(0f,-0.25f,-1f), 0.05f);
        }
        RaycastHit hitChest;
        // Debug.Log("Chest:" + Physics.Raycast(center.position, Vector3.back, out hitChest, 1f, chestLayer));
        if(Physics.Raycast(center2.position, Vector3.back, out hitChest, 1f, chestLayer) && !isCelerbrate){
            if(transform.position == chestTrans.position - new Vector3(0f,-0.25f,-1f)){
                Debug.Log("Chest");
                playerAnim.SetInteger("renwu", 2);
                isCelerbrate = true;
                Instantiate(openChestPrefab, hitChest.collider.gameObject.transform.position, Quaternion.Euler(new Vector3(-90f,0f,0f)));
                // Destroy()
                hitChest.collider.gameObject.SetActive(false);
                // hitChest.collider.gameObject.GetComponent<Chest>().OpenChest();
            }
        }
        CheckDiamond();
    }

    private void CheckDiamond(){
        RaycastHit hitDiamond;
        if(Physics.Raycast(center.position, Vector3.down, out hitDiamond, Mathf.Infinity, diamondLayer)){
            GameController.Instance.UpdateDiamondScore();
            Destroy(hitDiamond.collider.gameObject);
        }
    }

    private void CheckGetBrick(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, brickLayer)){
            Transform currentTransform = hit.collider.gameObject.transform;
            // hit.collider.gameObject.SetActive(false);
            Destroy(hit.collider.gameObject);
            brickHoldingList.Add(Instantiate(brickPrefab, currentTransform.position + brickCount * new Vector3(0f,0.25f,0f), Quaternion.identity, brickHolder.transform));
            PlayerObject.transform.position = currentTransform.position + brickCount * new Vector3(0f,0.25f,0f);
            brickCount++;
        }
    }

    private void MoveWithState(RunningState state){
        isRunning = false;
        StartCoroutine(PlayerRunning(state));
        // CheckDiamond();
    }

    private void UpScene(){
        SceneManager.LoadScene(GameController.Instance.levelNumber++);
    }

    private void MoveWithStateEnding(RunningState state){
        isRunning = false;
        StartCoroutine(PlayerEnding(state));
        // CheckDiamond();
    }

    private void CheckOnEnding(){
        if(CheckEndingState(RunningState.Up)){
            isEnding = true;
            MoveWithStateEnding(RunningState.Up);
        }
        else if(CheckEndingState(RunningState.Down)){
            isEnding = true;
            MoveWithStateEnding(RunningState.Down);
        }
        else if(CheckEndingState(RunningState.Left)){
            isEnding = true;
            MoveWithStateEnding(RunningState.Left);
        }
        else if(CheckEndingState(RunningState.Right)){
            isEnding = true;
            MoveWithStateEnding(RunningState.Right);
        }
    }

    IEnumerator PlayerEnding(RunningState state){
        if(CheckEndingState(state)){
            MoveToState(state);
            if(brickHoldingList.Count > 0){
                Destroy(brickHoldingList[brickHoldingList.Count-1]);
                brickHoldingList.RemoveAt(brickHoldingList.Count-1);
                brickCount--;
                PlayerObject.transform.position -= new Vector3(0f,0.25f,0f);
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(PlayerEnding(state));
            }
            else{
                Debug.Log("Lose");
            }
        }
        else{
            if(brickCount >= 1){
                MoveWithState(state);
                Destroy(brickHoldingList[brickHoldingList.Count-1]);
                brickHoldingList.RemoveAt(brickHoldingList.Count-1);
                brickCount--;
                PlayerObject.transform.position -= new Vector3(0f,0.25f,0f);
            }
            currentState = state;
            isRunning = true;
        }
    }

    IEnumerator PlayerRunning(RunningState state){
        if(CheckRunningState(state)){
            MoveToState(state);
            yield return new WaitForSeconds(0.1f);
            if(!CheckWining()){
                if(winCount > 0){
                    // playerAnim.SetInteger("renwu", 1);
                    brickHolder.SetActive(false);
                    Vector3 winPoint = new Vector3(PlayerObject.transform.position.x,-0.5f+2.8f,PlayerObject.transform.position.z);
                    PlayerObject.transform.position = winPoint;
                    isWinning = true;
                    Debug.Log("Win");
                }
                else{
                    StartCoroutine(PlayerRunning(state));
                }
            }
            else{
                winCount++;
                if(winCount < 5){
                    StartCoroutine(PlayerRunning(state));
                }
            }
        }
        else{
            currentState = state;
            if(Physics.Raycast(center.position, Vector3.down, Mathf.Infinity, pushLayer)){
                StartCoroutine(PlayerRunning(returnPurpleState(state)));
            }
            else{
                isRunning = true;
            }
            // CheckPushing();
            CheckOnEnding();
        }
    }

    private RunningState returnPurpleState(RunningState state){
        if(CheckRunningState(RunningState.Up) && currentState != RunningState.Down){
            return RunningState.Up;
        }
        else if(CheckRunningState(RunningState.Down) && currentState != RunningState.Up){
            return RunningState.Down;
        }
        else if(CheckRunningState(RunningState.Left) && currentState != RunningState.Right){
            return RunningState.Left;
        }
        else if(CheckRunningState(RunningState.Right) && currentState != RunningState.Left){
            return RunningState.Right;
        }
        return RunningState.None;
    }

    private void CheckPushing(){
        // RaycastHit hit;
        // if(Physics.Raycast(center.position, Vector3.down, out hit, Mathf.Infinity, pushLayer)){
        //     MoveWithState((RunningState)hit.collider.gameObject.GetComponent<PushPivot>().stateMove);
        // }
    }

    private void MoveToState(RunningState state){
        if(state == RunningState.Up){
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if(state == RunningState.Down){
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if(state == RunningState.Left){
            transform.position += new Vector3(1f, 0f, 0f);
        }
        else if(state == RunningState.Right){
            transform.position += new Vector3(-1f, 0f, 0f);
        }
    }

    private bool CheckWining(){
        return Physics.Raycast(up.position, Vector3.down, Mathf.Infinity, winLayer)
        || Physics.Raycast(down.position, Vector3.down, Mathf.Infinity, winLayer)
        || Physics.Raycast(left.position, Vector3.down, Mathf.Infinity, winLayer)
        || Physics.Raycast(right.position, Vector3.down, Mathf.Infinity, winLayer);
    }

    private bool CheckRunningState(RunningState state){
        if(state == RunningState.Up){
            return Physics.Raycast(up.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        else if(state == RunningState.Down){
            return Physics.Raycast(down.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        else if(state == RunningState.Left){
            return Physics.Raycast(left.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        else if(state == RunningState.Right){
            return Physics.Raycast(right.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        return false;
    }

    private bool CheckEndingState(RunningState state){
        // if(Physics.Raycast(center.position, Vector3.down, Mathf.Infinity, pushLayer)){
        //     return false;
        // }
        // else 
        if(state == RunningState.Up){
            return Physics.Raycast(up.position, Vector3.down, Mathf.Infinity, endLayer);
        }
        else if(state == RunningState.Down){
            return Physics.Raycast(down.position, Vector3.down, Mathf.Infinity, endLayer);
        }
        else if(state == RunningState.Left){
            return Physics.Raycast(left.position, Vector3.down, Mathf.Infinity, endLayer);
        }
        else if(state == RunningState.Right){
            return Physics.Raycast(right.position, Vector3.down, Mathf.Infinity, endLayer);
        }
        return false;
    }

}
