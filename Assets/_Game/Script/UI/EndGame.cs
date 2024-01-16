using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Rank;
    [SerializeField] TextMeshProUGUI Killer;
    [SerializeField] TextMeshProUGUI Coin;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;
    [SerializeField] Button Touch;


    //public void OnEnable()
    //{
    //    this.RegisterListener(EventID.Win, (param) => EnterWin());
    //    this.RegisterListener(EventID.Lose, (param) => EnterLose((string)param));
    //}

    public void RegisterListener()
    {
        this.RegisterListener(EventID.Win, (param) => EnterWin());
        this.RegisterListener(EventID.Lose, (param) => EnterLose((Character)param));
    }
    private void Start()
    {
        Touch.onClick.AddListener(() => BackMenu());
    }
    public void BackMenu()
    {
        SimplePool.CollectAll();
        UIManager.Instance.OpenMainMenu();
        LevelManager.Instance.player.ChangAnim(Constants.ANIM_IDLE);
        LevelManager.Instance.player.count.Cancel();
        LevelManager.Instance.player.OnInit();
    }
    public void EnterWin()
    {
        UIManager.Instance.OpenCanvasUI(GameState.EndGame);
        Rank.text = "#1";
        int score = LevelManager.Instance.player.score;
        Coin.text = score.ToString();
        Win.SetActive(true);
        Lose.SetActive(false);
        LevelManager.Instance.player.ChangAnim(Constants.ANIM_IDLE);
        LevelManager.Instance.player.OnInit();
        GameManager.Instance.UpdateCoin(score);
    }

    public void EnterLose(Character character)
    {
        UIManager.Instance.OpenCanvasUI(GameState.EndGame);
        Rank.text = "#" + LevelManager.Instance.GetCountAlive();
        int score = LevelManager.Instance.player.score;
        Coin.text = score.ToString();
        Killer.text = character.nameCharacter;
        Win.SetActive(false);
        Lose.SetActive(true);
        GameManager.Instance.UpdateCoin(score);



    }
}
