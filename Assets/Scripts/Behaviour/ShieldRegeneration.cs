using UnityEngine;

public class ShieldRegeneration : MonoBehaviour
{
    public static float lastHit = 0f;
    public static bool canRegenerate = false;
    public static float regenerationSpeed = 10f;

    private void FixedUpdate()
    {
        if (canRegenerate && Time.time > lastHit + regenerationSpeed)
        {
            PlayerManager.Instance.RegenerateShield();
        }
    }
}
