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

    private int ennemies = 3;
    private int ennemiesDestroyed = 0;
    private int wave = 1;
    private Vector2 center;
    private Vector2 size;
    private Transform canvas;

    public Image wavePanel;
    public Text waveText;
    public Ennemy ennemy;
    public Collider2D rectangle;
    public Player player;
    public Transform spawnPoint;
    public GameObject shop;
    public bool isPaused;

    private int hpMax = 10;
    private int spMax = 40;
    private int rockets = 3;
    private int score = 0;
    private int kills = 0;
    private int coins = 0;

    void Start()
    {
        center = rectangle.bounds.center;
        size = rectangle.bounds.size ;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

        StartCoroutine(ProcessWave(false));
    }

    private IEnumerator ProcessWave(bool hasReset)
    {
        isPaused = true;
        Time.timeScale = 0f;

        waveText.color = Color.red;

        if (hasReset)
        {
            waveText.text = "YOU HAVE LOST A LIFE. TRY AGAIN THIS WAVE!";

            for (float i = 0.05f; i <= 0.8f; i += 0.05f)
            {
                SetColor(i);
                yield return new WaitForSecondsRealtime(0.05f);
            }

            yield return new WaitForSecondsRealtime(2f);
        } else
        {
            CopyData();
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
            Vector2 pos = center + new Vector2(Random.Range(-size.x / 2 + 50f, size.x / 2 - 50f), Random.Range(-size.y / 2 + 50f, size.y / 2 - 50f));
            Instantiate(ennemy, pos, Quaternion.identity, canvas);
        }
    }

    private void CopyData()
    {
        var instance = PlayerManager.Instance;

        hpMax = instance.hpMax;
        spMax = instance.spMax;
        rockets = instance.rockets;
        score = instance.score;
        kills = instance.kills;
        coins = instance.coins;
    }

    public void ResetWave()
    {
        var instance = PlayerManager.Instance;

        instance.RestoreHealth(hpMax);
        instance.RestoreShield(spMax);
        instance.RestoreRockets(rockets);
        instance.RestoreScore(score);
        instance.RestoreKills(kills);
        instance.RestoreCoins(coins);

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

            PlayerManager.Instance.AddScore(100 * wave);

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

        PlayerManager.Instance.AddScore(50 * PlayerManager.Instance.coins);

        if (HighscoreManager.Instance.IsElligibleForHighscores(PlayerManager.Instance.score))
        {
            SceneManager.LoadScene(2);
        } else
        {
            SceneManager.LoadScene(0);
        }
    }
}
