using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void AddNewEntry(int score, string name)
    {
        HighscoreEntry entry = new HighscoreEntry { score = score, name = name };
        Highscores highscores = GetHighscores();

        highscores.list.RemoveAt(GetLowestScore(highscores));
        highscores.list.Add(entry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscores", json);
        PlayerPrefs.Save();
    }

    private int GetLowestScore(Highscores highscores)
    {
        int index = 0;
        float lowestScore = Mathf.Infinity;

        for (int i = 0; i < highscores.list.Count; i++)
        {
            if (highscores.list[i].score <= lowestScore)
            {
                lowestScore = highscores.list[i].score;
                index = i;
            }
        }

        return index;
    }

    public Highscores GetHighscores()
    {
        string defaultValue = "{\"list\":[{\"score\":10,\"name\":\"AAA\"},{\"score\":100,\"name\":\"AAA\"},{\"score\":5000,\"name\":\"AAA\"}]}";
        string jsonString = PlayerPrefs.GetString("highscores", defaultValue);

        return JsonUtility.FromJson<Highscores>(jsonString);
    }

    public bool IsElligibleForHighscores(int score)
    {
        Highscores highscores = GetHighscores();

        foreach (HighscoreEntry entry in highscores.list)
        {
            if (score >= entry.score)
            {
                return true;
            }
        }

        return false;
    }
}
