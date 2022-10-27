using UnityEngine;
using UnityEngine.SceneManagement;

// Class to control scene changes
public class SceneTransition : MonoBehaviour
{
    // Load main menu
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        GameManager.ResetWave();
    }

    // Quiz application
    public void QuitGame()
    {
        Application.Quit();
    }

    // Load first map
    public void Map1Game()
    {
        SceneManager.LoadScene(1);
    }

    // Load second map
    public void Map2Game()
    {
        SceneManager.LoadScene(2);
    }

    // Load third map
    public void Map3Game()
    {
        SceneManager.LoadScene(3);
    }

    // Restart game and reset values
    public void RestartGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        GameManager.ResetWave();
    }
}
