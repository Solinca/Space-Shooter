using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public void Start()
    {
        GameObject[] lifes = GameObject.FindGameObjectsWithTag("Life");
        PlayerManager.Instance.lifesContainer = lifes;

        lifes[3].SetActive(false);
        lifes[4].SetActive(false);
        lifes[5].SetActive(false);
    }
}
