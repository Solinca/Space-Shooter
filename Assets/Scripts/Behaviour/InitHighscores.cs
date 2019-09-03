using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitHighscores : MonoBehaviour
{
    public GameObject template;
    public Transform container;

    private void Awake()
    {
        Highscores highscores = HighscoreManager.Instance.GetHighscores();
        List<HighscoreEntry> highscoreList = highscores.list;

        for (int i = 0; i < highscoreList.Count; i++)
        {
            for (int j = i + 1; j < highscoreList.Count; j++)
            {
                if (highscoreList[j].score > highscoreList[i].score)
                {
                    var temp = highscoreList[i];
                    highscoreList[i] = highscoreList[j];
                    highscoreList[j] = temp;
                }
            }
        }

        List<Transform> highscoreTransformList = new List<Transform>();

        foreach (HighscoreEntry entry in highscoreList)
        {
            CreateHighscoreEntry(entry, highscoreTransformList);
        }
    }

    private void CreateHighscoreEntry(HighscoreEntry entry, List<Transform> list)
    {
        float templateHeight = 70f;
        GameObject newEntry = Instantiate(template, container);
        RectTransform newEntryTransform = newEntry.GetComponent<RectTransform>();
        newEntryTransform.anchoredPosition = new Vector2(0, -list.Count * templateHeight + 5f);
        newEntry.gameObject.SetActive(true);

        int rank = list.Count + 1;
        string rankName = "";

        switch (rank)
        {
            case 1:
                rankName = "1st";
                break;
            case 2:
                rankName = "2nd";
                break;
            case 3:
                rankName = "3rd";
                break;
            default:
                break;
        }

        newEntryTransform.Find("Position").GetComponent<Text>().text = rankName;
        newEntryTransform.Find("Score").GetComponent<Text>().text = entry.score.ToString();
        newEntryTransform.Find("Name").GetComponent<Text>().text = entry.name;

        list.Add(newEntryTransform);
    }
}
