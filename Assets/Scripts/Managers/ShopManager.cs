using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

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

    public Button addOneLife;
    public Button refillRockets;
    public Button addOneRocket;
    public Button addShieldRechargeRate;
    public Button addTenShield;
    public readonly int addOneLifeCost = 10;
    public readonly int addOneRocketCost = 5;
    public readonly int upgradeShieldCapacityCost = 5;
    public readonly int upgradeShieldRegenerationCost = 5;
    public readonly int refillRocketsCost = 3;

    private int lifeUpgrade = 0;
    private int rocketUpgrade = 0;
    private int shieldRechargeRateUpgrade = 0;
    private int shieldCapacityUpgrade = 0;
    private readonly int maxLifeUpgrade = 3;
    private readonly int maxRocketsUpgrade = 7;
    private readonly int maxShieldCapacityUpgrade = 6;
    private readonly int maxShieldRechargeRateUpgrade = 16;

    private void OnEnable()
    {
        CheckPrice();
    }

    public void RefillRockets()
    {
        PlayerManager.Instance.RefillRockets();
    }

    public void AddOneLife()
    {
        lifeUpgrade++;
        PlayerManager.Instance.AddOneLife();
    }

    public void AddOneRocket()
    {
        rocketUpgrade++;
        PlayerManager.Instance.AddOneRocket();
    }

    public void UpgradeShieldRegenerationRate()
    {
        shieldRechargeRateUpgrade++;
        PlayerManager.Instance.UpgradeShieldRegenerationRate();
    }

    public void UpgradeShieldCapacity()
    {
        shieldCapacityUpgrade++;
        PlayerManager.Instance.UpgradeShieldCapacity();
    }

    public void CheckPrice()
    {
        int coins = PlayerManager.Instance.coins;

        addOneLife.interactable = true;
        addOneRocket.interactable = true;
        addShieldRechargeRate.interactable = true;
        addTenShield.interactable = true;
        refillRockets.interactable = true;

        if (coins < addOneLifeCost)
        {
            addOneLife.interactable = false;

            if (coins < addOneRocketCost)
            {
                addOneRocket.interactable = false;
                addShieldRechargeRate.interactable = false;
                addTenShield.interactable = false;

                if (coins < refillRocketsCost)
                {
                    refillRockets.interactable = false;
                }
            }
        }

        if (lifeUpgrade == maxLifeUpgrade)
        {
            addOneLife.interactable = false;
        }

        if (rocketUpgrade == maxRocketsUpgrade)
        {
            addOneRocket.interactable = false;
        }

        if (shieldCapacityUpgrade == maxShieldCapacityUpgrade)
        {
            addTenShield.interactable = false;
        }

        if (shieldRechargeRateUpgrade == maxShieldRechargeRateUpgrade)
        {
            addShieldRechargeRate.interactable = false;
        }
    }
}
