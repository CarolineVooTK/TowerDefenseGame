using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Map1Game()
    {
        SceneManager.LoadScene(1);
    }

    public void Map2Game()
    {
        SceneManager.LoadScene(2);
    }

    public void Map3Game()
    {
        SceneManager.LoadScene(3);
    }

    public void RestartGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
