using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public void Start()
    {
        PlayerManager.Instance.lifesContainer = GameObject.FindGameObjectsWithTag("Life");
        PlayerManager.Instance.ManageLife();
    }
}
