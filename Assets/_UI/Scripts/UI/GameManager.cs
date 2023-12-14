using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public enum ColorType {
    Default,
    Red,
    Yellow,
    Green,
    Blue,
    Purple, 
    Orange,
    Gray
}

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;

    [SerializeField] private Transform startPoint;
    [SerializeField] private Player player;
    [SerializeField] private Bot botPrefab;
    [SerializeField] private Transform finishPoint;
    
    private List<ColorType> usedColors;
    private List<Bot> bots = new List<Bot>();

    public List<ColorType> UsedColors { get => usedColors; }
    public int botAmount;
    public int CharacterAmount => botAmount + 1;
    public Vector3 FinishPoint => finishPoint.position;

    // Start is called before the first frame update
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = Screen.currentResolution.width / Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

        // UIManager.Ins.OpenUI<MianMenu>();

        if (usedColors == null)
        {
            usedColors = new List<ColorType>();
            for (int i = 0; i <= botAmount; i++) {
                usedColors.Add((ColorType) i);
            }
        }
    }

    private void Start() {
        OnInit();
    }

    public void OnInit() {

        List<Vector3> startPoints = new List<Vector3>();

        for (int i = 0; i < CharacterAmount; i++) {
            startPoints.Add(startPoint.position + Vector3.right * 3f * i);
        }

        int randPosition = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[randPosition];
        startPoints.RemoveAt(randPosition);
        
        for (int i = 0; i < CharacterAmount - 1; i++) {
            Bot bot = Instantiate(botPrefab, startPoints[i], Quaternion.identity).GetComponent<Bot>(); 
            bots.Add(bot);
            bot.ChangeState(new PatrolState());
            bot.SetDestination(bot.transform.position);
        }
    }

    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}

}
