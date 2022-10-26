using UnityEngine;

// Class to stop time
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    // Start time
    public void Resume()
    {
        GamePaused = false;
        Time.timeScale = 1f;
    }

    // Stop time
    public void Pause()
    {
        Time.timeScale = 0f;
        GamePaused = true;
    }
}
