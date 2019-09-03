using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public new AudioSource audio;

    private readonly float autoDestructionTime = 3f;
    private bool collected = false;

    private void Start()
    {
        Destroy(gameObject, autoDestructionTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null && !collected)
        {
            collected = true;
            PlayerManager.Instance.AddCoin();
            StartCoroutine(PlayAudio());
        }
    }

    private IEnumerator PlayAudio()
    {
        audio.Play();
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
