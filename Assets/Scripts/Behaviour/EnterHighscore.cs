using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterHighscore : MonoBehaviour
{
    public InputField playerName;

    private void Start()
    {
        playerName.Select();
    }

    public void OnValueChange(string value)
    {
        playerName.text = playerName.text.ToUpper();
    }

    public void ReturnToMainMenu()
    {
        HighscoreManager.Instance.AddNewEntry(PlayerManager.Instance.score, playerName.text);
        SceneManager.LoadScene(0);
    }
}
