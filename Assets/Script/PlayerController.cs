using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public Vector3 startPoint, resetPoint;
    public GameObject startGO, endGO;
    [Header("Bai 3")]
    public List<GameObject> targetList;
    private int idxTarget = 0;
    [Header("Bai 4 - Error")]
    public GameObject startGPoint, endGPoint;
    [Header("Bai 5")]
    public List<GameObject> targetList5;
    private int idxTarget5 = 1;
    [Header("Bai 7")]
    public List<GameObject> targetList7;
    private int idxTarget7 = 0;
    [Header("Bai 8")]
    public List<GameObject> targetList8;
    private bool canMove = true;
    private int idxTarget8 = 1;
    [Header("Bai 9")]
    public List<GameObject> targetList9;
    private int idxTarget9 = 1;
    private float desiredDuration9 = 3f;
    private float elapsedTime9;
    [Header("Bai 10")]
    public List<GameObject> targetList10;
    private int idxTarget10 = 1;
    private float desiredDuration10 = 3f;
    private float elapsedTime10;

    // Start is called before the first frame update
    void Start()
    {
        // startPoint = transform.position;
        // targetList[idxTarget].GetComponent<BoxCollider2D>().enabled = true;
        // transform.position = startGPoint.transform.position;

        // transform.position = targetList5[0].transform.position;
        // targetList5[idxTarget5].GetComponent<BoxCollider2D>().enabled = true;

        // idxTarget7 = Random.Range(0, targetList7.Count);
        // targetList7[idxTarget7].GetComponent<BoxCollider2D>().enabled = true;

        // transform.position = targetList8[0].transform.position;
        // targetList8[idxTarget8].GetComponent<BoxCollider2D>().enabled = true;
        // Invoke("ActiveMove", 1f);

        // transform.position = targetList9[1-idxTarget9].transform.position;
        // targetList9[idxTarget9].GetComponent<BoxCollider2D>().enabled = true;

        // transform.position = targetList10[1-idxTarget10].transform.position;
        // targetList10[idxTarget10].GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Bai 6
        if(Input.GetKey("w")){
            // transform.Translate
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if(Input.GetKey("a")){
            // transform.Translate
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if(Input.GetKey("s")){
            // transform.Translate
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if(Input.GetKey("d")){
            // transform.Translate
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        // if(transform.position.y >= resetPoint.y){
        //     transform.position = startPoint;
        // }

        // if(absNumber(gameObject.transform.position.x, endGO.transform.position.x) <= 0.3f
        // && absNumber(gameObject.transform.position.y, endGO.transform.position.y) <= 0.3f
        // ){
        //     gameObject.transform.position = startGO.transform.position;
        //     // pri
        // }
        // Bai 3
        transform.position = Vector3.MoveTowards(transform.position, targetList[idxTarget].transform.position, 0.01f);
        // Bai 4 - Error
        // transform.position = Vector3.Slerp(transform.position, endGPoint.transform.position, 1.5f);
        // Bai 5
        // transform.position = Vector3.MoveTowards(transform.position, targetList5[idxTarget5].transform.position, 0.3f);
        // Bai 7
        // transform.position = Vector3.MoveTowards(transform.position, targetList7[idxTarget7].transform.position, 0.01f);
        // Bai 8
        // if(canMove){
        //     transform.position = Vector3.MoveTowards(transform.position, targetList8[idxTarget8].transform.position, 0.01f);
        // }
        // Bai 9
        // elapsedTime9 += Time.deltaTime;
        // float percentageComplete9 = elapsedTime9 / desiredDuration9;
        // transform.position = Vector3.Lerp(targetList9[1-idxTarget9].transform.position, targetList9[idxTarget9].transform.position, percentageComplete9);
        // Bai 10
        // elapsedTime10 += Time.deltaTime;
        // float percentageComplete10 = elapsedTime10 / desiredDuration10;
        // transform.position = Vector3.Lerp(targetList10[1-idxTarget10].transform.position, targetList10[idxTarget10].transform.position, percentageComplete10);


    }

    private float absNumber(float x, float y){
        if(x-y > 0){
            return x-y;
        }
        return y-x;
    }

    private void ActiveMove(){
        canMove = !canMove;
        Invoke("ActiveMove", 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "End"){
            // gameObject.transform.position = startGO.transform.position;

            // targetList[idxTarget].GetComponent<BoxCollider2D>().enabled = false;
            idxTarget++;
            if(idxTarget == targetList.Count){
                idxTarget = 0;
            }
            // targetList[idxTarget].GetComponent<BoxCollider2D>().enabled = true;

            // targetList5[idxTarget5].GetComponent<BoxCollider2D>().enabled = false;
            // idxTarget5++;
            // if(idxTarget5 >= targetList5.Count){
            //     idxTarget5 = 0;
            // }
            // targetList5[idxTarget5].GetComponent<BoxCollider2D>().enabled = true;

            targetList7[idxTarget7].GetComponent<BoxCollider2D>().enabled = false;
            idxTarget7 = Random.Range(0, targetList7.Count);
            targetList7[idxTarget7].GetComponent<BoxCollider2D>().enabled = true;

            // targetList8[idxTarget8].GetComponent<BoxCollider2D>().enabled = false;
            // idxTarget8++;
            // if(idxTarget8 >= targetList8.Count){
            //     idxTarget8 = 0;
            // }
            // targetList8[idxTarget8].GetComponent<BoxCollider2D>().enabled = true;

            // targetList9[idxTarget9].GetComponent<BoxCollider2D>().enabled = false;
            // idxTarget9 = 1 - idxTarget9;
            // elapsedTime9 = 0;
            // targetList9[idxTarget9].GetComponent<BoxCollider2D>().enabled = true;
            
            // StartCoroutine(TakeABreak());

        }
    }

    private IEnumerator TakeABreak(){
        yield return new WaitForSeconds(Random.Range(1f,2f));
        targetList10[idxTarget10].GetComponent<BoxCollider2D>().enabled = false;
        idxTarget10 = 1 - idxTarget10;
        elapsedTime10 = 0;
        targetList10[idxTarget10].GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }


}
