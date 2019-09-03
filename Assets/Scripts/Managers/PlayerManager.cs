using System.Reflection;
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
    public int lifeUpgrade = 0;

    public int hp = 10;
    public int hpMax = 10;
    public Text hpText;

    public int sp = 40;
    public int spMax = 40;
    public int spUpgrade = 0;
    public int spRechargeRateUpgrade = 0;
    public Text spText;

    public int rockets = 3;
    public int rocketsMax = 3;
    public int rocketUpgrade = 0;
    public Text rocketsText;

    public int score = 0;
    public Text scoreText;

    public int kills = 0;
    public Text killsText;

    public int coins = 0;
    public Text coinsText;

    public GameObject[] lifesContainer;

    public void AddCoin()
    {
        Pay(-1);
    }

    public void Pay(int amount)
    {
        coins -= amount;
        UpdateInput(coins, coinsText);
    }

    public void AddKill()
    {
        AddScore(10);
        UpdateInput(++kills, killsText);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateInput(score, scoreText);
    }

    public void RefillRockets()
    {
        rockets = rocketsMax;
        UpdateInput(rockets, rocketsMax, rocketsText);

        Pay(3);
    }

    public void AddOneRocket()
    {
        rocketUpgrade++;
        UpdateInput(++rockets, ++rocketsMax, rocketsText);

        Pay(5);
    }

    public bool CanShootRocket()
    {
        return rockets > 0;
    }

    public void ConsumeOneRocket()
    {
        UpdateInput(--rockets, rocketsMax, rocketsText);
    }

    public void LoseHealth(int damage)
    {
        if (sp > 0)
        {
            sp -= damage;
            UpdateInput(sp, spMax, spText);
        } else
        {
            hp -= damage;
            UpdateInput(hp, hpMax, hpText);
        }

        ShieldRegeneration.canRegenerate = true;
        ShieldRegeneration.lastHit = Time.time;

        if (hp <= 0)
        {
            lifes--;
            ManageLife();

            if (lifes > 0)
            {
                WaveManager.Instance.ResetWave();
            } else
            {
                WaveManager.Instance.GameOver();
            }
        }
    }

    public void AddTenShield()
    {
        sp += 10;
        spMax += 10;
        spUpgrade++;

        UpdateInput(sp, spMax, spText);

        Pay(5);
    }

    public void UpgradeShieldRegenerationRate()
    {
        spRechargeRateUpgrade++;
        ShieldRegeneration.regenerationSpeed -= 0.5f;

        Pay(5);
    }

    public void RegenerateShield()
    {
        UpdateInput(++sp, spMax, spText);

        if (sp == spMax)
        {
            ShieldRegeneration.canRegenerate = false;
        }
    }

    public void AddOneLife()
    {
        lifes++;
        lifeUpgrade++;

        Pay(10);

        ManageLife();
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

    public void RestoreHealth(int value)
    {
        hp = value;
        UpdateInput(value, hpMax, hpText);
    }

    public void RestoreShield(int value)
    {
        sp = value;
        UpdateInput(value, spMax, spText);
    }

    public void RestoreRockets(int value)
    {
        rockets = value;
        UpdateInput(value, rocketsMax, rocketsText);
    }

    public void RestoreScore(int value)
    {
        score = value;
        UpdateInput(value, scoreText);
    }

    public void RestoreKills(int value)
    {
        kills = value;
        UpdateInput(value, killsText);
    }

    public void RestoreCoins(int value)
    {
        coins = value;
        UpdateInput(value, coinsText);
    }

    public void UpdateInput(int value, Text text)
    {
        text.text = value.ToString();
    }

    public void UpdateInput(int value, int max, Text text)
    {
        text.text = value.ToString() + " / " + max.ToString();
    }

    public void ResetValues()
    {
        lifes = 3;
        lifeUpgrade = 0;
        hp = 10;
        hpMax = 10;
        sp = 40;
        spMax = 40;
        spUpgrade = 0;
        spRechargeRateUpgrade = 0;
        rockets = 3;
        rocketsMax = 3;
        rocketUpgrade = 0;
        kills = 0;
        score = 0;
        coins = 0;

        ShieldRegeneration.canRegenerate = false;
    }
}
