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

    private void Awake()
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
    }
    public void EnterWin()
    {
        UIManager.Instance.OpenCanvasUI(GameState.EndGame);
        Rank.text = "#1";
        Coin.text = LevelManager.Instance.player.score.ToString();
        Win.SetActive(true);
        Lose.SetActive(false);

        LevelManager.Instance.player.ChangAnim(Constants.ANIM_IDLE);
    }

    public void EnterLose(Character killer)
    {
        UIManager.Instance.OpenCanvasUI(GameState.EndGame);
        Rank.text = "#" + LevelManager.Instance.GetCountAlive();
        Coin.text = LevelManager.Instance.player.score.ToString();
        Killer.text = killer.nameCharacter;
        Win.SetActive(false);
        Lose.SetActive(true);
        

    }
}
