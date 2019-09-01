using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

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

    public int lifes = 3;

    public int hp = 10;
    public int hpMax = 10;
    public Text hpText;

    public int sp = 40;
    public int spMax = 40;
    public Text spText;

    public int rockets = 3;
    public int rocketsMax = 3;
    public Text rocketText;

    public int score = 0;
    public Text scoreText;

    public int kills = 0;
    public Text killsText;

    public int coins = 0;
    public Text coinsText;

    public GameObject[] lifesContainer; 

    public void AddCoin()
    {
        coins++;
        coinsText.text = coins.ToString();
    }

    public void AddKill()
    {
        kills++;
        killsText.text = kills.ToString();

        score += 10;
        scoreText.text = score.ToString();
    }

    public void LoseHealth(int damage)
    {
        if (sp > 0)
        {
            sp -= damage;
            spText.text = sp.ToString() + " / " + spMax.ToString();
        } else
        {
            hp -= damage;
            hpText.text = hp.ToString() + " / " + hpMax.ToString();
        }

        ShieldRegeneration.canRegenerate = true;
        ShieldRegeneration.lastHit = Time.time;

        if (hp <= 0)
        {
            lifes--;
            ManageLife();
        }
    }

    public void RegenerateShield()
    {
        sp++;
        spText.text = sp.ToString() + " / " + spMax.ToString();

        if (sp == spMax)
        {
            ShieldRegeneration.canRegenerate = false;
        }
    }

    public void ManageLife()
    {
        for (var i = 0; i < lifesContainer.Length; i++)
        {
            if (i < lifes)
            {
                lifesContainer[i].SetActive(true);
            } else
            {
                lifesContainer[i].SetActive(false);
            }
        }
    }

    public void ResetValues()
    {
        lifes = 3;
        hp = 10;
        hpMax = 10;
        sp = 40;
        spMax = 40;
        rockets = 3;
        rocketsMax = 3;
        kills = 0;
        score = 0;
        coins = 0;
    }
}
