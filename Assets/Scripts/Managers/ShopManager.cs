using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button addOneLife;
    public Button refillRockets;
    public Button addOneRocket;
    public Button addShieldRechargeRate;
    public Button addTenShield;

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
        PlayerManager.Instance.AddOneLife();
    }

    public void AddOneRocket()
    {
        PlayerManager.Instance.AddOneRocket();
    }

    public void UpgradeShieldRegenerationRate()
    {
        PlayerManager.Instance.UpgradeShieldRegenerationRate();
    }

    public void AddTenShield()
    {
        PlayerManager.Instance.AddTenShield();
    }

    public void CheckPrice()
    {
        int coins = PlayerManager.Instance.coins;

        addOneLife.interactable = true;
        addOneRocket.interactable = true;
        addShieldRechargeRate.interactable = true;
        addTenShield.interactable = true;
        refillRockets.interactable = true;

        if (coins < 10)
        {
            addOneLife.interactable = false;

            if (coins < 5)
            {
                addOneRocket.interactable = false;
                addShieldRechargeRate.interactable = false;
                addTenShield.interactable = false;

                if (coins < 3)
                {
                    refillRockets.interactable = false;
                }
            }
        }

        if (PlayerManager.Instance.lifeUpgrade == maxLifeUpgrade)
        {
            addOneLife.interactable = false;
        }

        if (PlayerManager.Instance.rocketUpgrade == maxRocketsUpgrade)
        {
            addOneRocket.interactable = false;
        }

        if (PlayerManager.Instance.spUpgrade == maxShieldCapacityUpgrade)
        {
            addTenShield.interactable = false;
        }

        if (PlayerManager.Instance.spRechargeRateUpgrade == maxShieldRechargeRateUpgrade)
        {
            addShieldRechargeRate.interactable = false;
        }
    }
}
