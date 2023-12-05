using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool _gamePaused;
    public static bool IsGamePaused
    {
        get => _gamePaused;
        set
        {
            _gamePaused = value;
            Time.timeScale = _gamePaused ? 0f : 1f;
        }
    }
}
