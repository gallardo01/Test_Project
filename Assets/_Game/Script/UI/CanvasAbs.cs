using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAbs : MonoBehaviour
{
    public virtual void BackToMainMenu()
    {
        SoundManager.Instance.StopSound();
        UIManager.Instance.TurnOnCoinCanvas();
        UIManager.Instance.OpenCanvasUI(GameState.MainMenu);
        LevelManager.Instance.OffCircleAttack();

    }

    public virtual void ChangeVolume()
    {
        SoundManager.Instance.ChangeVolume();
    }
}
