using UnityEngine;

public class AppUtil : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StopGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ReStartGame()
    {
        Time.timeScale = 1;
    }
}
