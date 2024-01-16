using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] Button Home;
    [SerializeField] Button Continue;
    [SerializeField] Button Volume;

    private void Start()
    {
        Home.onClick.AddListener(() => BackMenu());
        Continue.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.GamePlay));
        Volume.onClick.AddListener(() => ChangeVolume());

    }
    public void BackMenu()
    {
        SimplePool.CollectAll();
        UIManager.Instance.OpenMainMenu();
        LevelManager.Instance.player.ChangAnim(Constants.ANIM_IDLE);
        LevelManager.Instance.player.count.Cancel();
    }

    public void ChangeVolume()
    {
        bool check = SoundManager.Instance.IsMute;
        SoundManager.Instance.IsMute = !check;
        AudioListener.volume = SoundManager.Instance.IsMute ? 0 : 1;
    }
}
