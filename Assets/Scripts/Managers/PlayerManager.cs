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

    public Text hpText;
    public Text spText;
    public Text rocketsText;
    public Text scoreText;
    public Text killsText;
    public Text coinsText;
    public GameObject[] lifesContainer;
    public int score = 0;
    public int coins = 0;

    private int lifes = 3;
    private int hp = 10;
    private int hpMax = 10;
    private int sp = 40;
    private int spMax = 40;
    private int rockets = 3;
    private int rocketsMax = 3;
    private int kills = 0;
    private int hpMaxCopy = 10;
    private int spMaxCopy = 40;
    private int rocketsCopy = 3;
    private int scoreCopy = 0;
    private int killsCopy = 0;
    private int coinsCopy = 0;
    private readonly int coinValue = 1;
    private readonly int coinScorePoint = 50;
    private readonly int killScoreValue = 10;
    private readonly int upgradeShieldCapacityValue = 10;
    private readonly float upgradeShieldRegenerationValue = 0.5f;

    public void AddCoin()
    {
        Pay(-coinValue);
    }

    public void Pay(int amount)
    {
        coins -= amount;
        UpdateInput(coins, coinsText);
    }

    public void AddKill()
    {
        AddScore(killScoreValue);
        UpdateInput(++kills, killsText);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateInput(score, scoreText);
    }

    public void AddCoinScore()
    {
        score += coinScorePoint * coins;
    }

    public void RefillRockets()
    {
        rockets = rocketsMax;
        UpdateInput(rockets, rocketsMax, rocketsText);

        Pay(ShopManager.Instance.refillRocketsCost);
    }

    public void AddOneRocket()
    {
        UpdateInput(++rockets, ++rocketsMax, rocketsText);

        Pay(ShopManager.Instance.addOneRocketCost);
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
            lifesContainer[lifes].SetActive(false);

            if (lifes > 0)
            {
                WaveManager.Instance.ResetWave();
            } else
            {
                WaveManager.Instance.GameOver();
            }
        }
    }

    public void UpgradeShieldCapacity()
    {
        sp += upgradeShieldCapacityValue;
        spMax += upgradeShieldCapacityValue;

        UpdateInput(sp, spMax, spText);

        Pay(ShopManager.Instance.upgradeShieldCapacityCost);
    }

    public void UpgradeShieldRegenerationRate()
    {
        ShieldRegeneration.regenerationSpeed -= upgradeShieldRegenerationValue;

        Pay(ShopManager.Instance.upgradeShieldRegenerationCost);
    }

    public void RegenerateShield()
    {
        UpdateInput(++sp, spMax, spText);

        if (sp == spMax)
        {
            ShieldRegeneration.canRegenerate = false;
        }
    }

    public void ResetLifeAndShield()
    {
        ShieldRegeneration.canRegenerate = false;

        hp = hpMax;
        sp = spMax;

        UpdateInput(sp, spMax, spText);
        UpdateInput(hp, hpMax, hpText);
    }

    public void AddOneLife()
    {
        lifesContainer[lifes].SetActive(true);
        lifes++;

        Pay(ShopManager.Instance.addOneLifeCost);
    }

    public void UpdateInput(int value, Text text)
    {
        text.text = value.ToString();
    }

    public void UpdateInput(int value, int max, Text text)
    {
        text.text = value.ToString() + " / " + max.ToString();
    }

    public void CopyData()
    {
        hpMaxCopy = hpMax;
        spMaxCopy = spMax;
        rocketsCopy = rockets;
        scoreCopy = score;
        killsCopy = kills;
        coinsCopy = coins;
    }

    public void RestoreData()
    {
        hp = hpMaxCopy;
        sp = spMaxCopy;
        rockets = rocketsCopy;
        score = scoreCopy;
        kills = killsCopy;
        coins = coinsCopy;

        UpdateInput(hpMaxCopy, hpMax, hpText);
        UpdateInput(spMaxCopy, spMax, spText);
        UpdateInput(rocketsCopy, rocketsMax, rocketsText);
        UpdateInput(scoreCopy, scoreText);
        UpdateInput(killsCopy, killsText);
        UpdateInput(coinsCopy, coinsText);
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

        ShieldRegeneration.canRegenerate = false;
    }
}
