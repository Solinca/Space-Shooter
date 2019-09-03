using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public Image wavePanel;
    public Text waveText;
    public Ennemy ennemy;
    public Collider2D rectangle;
    public Player player;
    public Transform spawnPoint;
    public GameObject shop;
    public bool isPaused;

    private int ennemies = 3;
    private int ennemiesDestroyed = 0;
    private int wave = 1;
    private readonly float mapMargin = 50f;
    private readonly int waveScorePoint = 100;
    private Vector2 center;
    private Vector2 size;
    private Transform canvas;

    void Start()
    {
        center = rectangle.bounds.center;
        size = rectangle.bounds.size ;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        isPaused = true;
    }

    public void LaunchGame()
    {
        StartCoroutine(ProcessWave(false));
    }

    private IEnumerator ProcessWave(bool hasReset)
    {
        isPaused = true;
        Time.timeScale = 0f;

        waveText.color = Color.red;

        if (hasReset)
        {
            waveText.text = "YOU HAVE LOST A LIFE.\nTRY THIS WAVE AGAIN!";

            for (float i = 0.05f; i <= 0.8f; i += 0.05f)
            {
                SetColor(i);
                yield return new WaitForSecondsRealtime(0.05f);
            }

            yield return new WaitForSecondsRealtime(2f);
        } else
        {
            PlayerManager.Instance.CopyData();
        }
        
        SpawnEnnemies();
        SpawnPlayer();

        waveText.text = "WAVE " + wave.ToString();

        if (!hasReset)
        {
            for (float i = 0.05f; i <= 0.8f; i += 0.05f)
            {
                SetColor(i);
                yield return new WaitForSecondsRealtime(0.05f);
            }
        }

        yield return new WaitForSecondsRealtime(2f);

        for (float i = 0.8f; i >= 0f; i -= 0.05f)
        {
            SetColor(i);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        SetColor(0f);

        isPaused = false;

        if (!PauseMenu.isPaused)
        {
            Time.timeScale = 1f;
        }
    }

    private void SetColor(float i)
    {
        Color color = waveText.color;
        Color color2 = wavePanel.color;
        color.a = color2.a = i;
        waveText.color = color;
        wavePanel.color = color2;
    }

    private void SpawnPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = Quaternion.identity;
    }

    private void SpawnEnnemies()
    {
        for (var i = 0; i < ennemies; i++)
        {
            float randomX = Random.Range(-size.x / 2 + mapMargin, size.x / 2 - mapMargin);
            float randomY = Random.Range(-size.y / 2 + mapMargin, size.y / 2 - mapMargin);
            Vector2 pos = center + new Vector2(randomX, randomY);
            Instantiate(ennemy, pos, Quaternion.identity, canvas);
        }
    }

    public void ResetWave()
    {
        PlayerManager.Instance.RestoreData();

        ennemiesDestroyed = 0;

        CleanScene(true);

        StartCoroutine(ProcessWave(true));
    }

    public void CleanScene(bool withEnnemyAndCoins)
    {
        if (withEnnemyAndCoins)
        {
            var ennemiesObject = GameObject.FindGameObjectsWithTag("Ship");

            foreach (GameObject ennemyObject in ennemiesObject)
            {
                if (ennemyObject.GetComponent<Player>() == null)
                {
                    Destroy(ennemyObject);
                }
            }

            var coinsObject = GameObject.FindGameObjectsWithTag("Coin");

            foreach (GameObject coinObject in coinsObject)
            {
                Destroy(coinObject);
            }
        }

        var rocketsObject = GameObject.FindGameObjectsWithTag("Rocket");

        foreach (GameObject rocketObject in rocketsObject)
        {
            Destroy(rocketObject);
        }

        var bulletsObject = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bulletObject in bulletsObject)
        {
            Destroy(bulletObject);
        }
    }

    public void DestroyedEnnemy()
    {
        ennemiesDestroyed++;

        if (ennemiesDestroyed == ennemies) {
            CleanScene(false);

            PlayerManager.Instance.AddScore(waveScorePoint * wave);
            PlayerManager.Instance.ResetLifeAndShield();

            StartCoroutine(EndWave());
        }
    }

    private IEnumerator EndWave()
    {
        yield return new WaitForSecondsRealtime(3f);

        isPaused = true;

        waveText.text = "WELL DONE!";
        waveText.color = Color.green;

        for (float i = 0.05f; i <= 0.8f; i += 0.05f)
        {
            SetColor(i);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        yield return new WaitForSecondsRealtime(2f);

        for (float i = 0.8f; i >= 0f; i -= 0.05f)
        {
            SetColor(i);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        SetColor(0f);

        shop.SetActive(true);
    }

    public void NextWave()
    {
        wave++;
        ennemies++;
        ennemiesDestroyed = 0;

        shop.SetActive(false);

        StartCoroutine(ProcessWave(false));
    }

    public void GameOver()
    {
        CleanScene(true);

        StartCoroutine(EndScreen());
    }

    private IEnumerator EndScreen()
    {
        isPaused = true;

        waveText.text = "GAME OVER :(";

        for (float i = 0.05f; i <= 0.8f; i += 0.05f)
        {
            SetColor(i);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        yield return new WaitForSecondsRealtime(2f);

        PlayerManager.Instance.AddCoinScore();

        if (HighscoreManager.Instance.IsElligibleForHighscores(PlayerManager.Instance.score))
        {
            SceneManager.LoadScene("EnterHighscore");
        } else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
