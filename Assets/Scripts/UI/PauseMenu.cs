using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public void Resume()
    {
        GamePaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GamePaused = true;
    }
}
