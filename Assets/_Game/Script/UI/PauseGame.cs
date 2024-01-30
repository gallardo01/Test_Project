using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : CanvasAbs
{
    [SerializeField] Button Home;
    [SerializeField] Button Continue;
    [SerializeField] Button Volume;
    [SerializeField] Image Sound;
    [SerializeField] Image Mute;

    private void Start()
    {
        Home.onClick.AddListener(() => BackToMainMenu());
        Continue.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.GamePlay));
        Volume.onClick.AddListener(ChangVolume);
        
    }
    private void OnEnable()
    {
        ChangeImage();
    }
    public override void BackToMainMenu()
    {
        base.BackToMainMenu();
        SimplePool.CollectAll();
        LevelManager.Instance.player.OnInit();
        LevelManager.Instance.player.ChangAnim(Constants.ANIM_IDLE);
        LevelManager.Instance.player.count.Cancel();
        LevelManager.Instance.timAirDrop.Cancel();
        LevelManager.Instance.OffAirDrop();
    }

    public override void ChangVolume()
    {
        base.ChangVolume();
        ChangeImage();
    }
    private void ChangeImage()
    {
        if(SoundManager.Instance.IsMuted()) {
            Sound.gameObject.SetActive(false);
            Mute.gameObject.SetActive(true);
        }
        else
        {
            Sound.gameObject.SetActive(true);
            Mute.gameObject.SetActive(false);
        }
    }


}
