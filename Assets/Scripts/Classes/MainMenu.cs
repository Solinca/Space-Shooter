using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");

        if (PlayerManager.Instance)
        {
            PlayerManager.Instance.ResetValues();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
