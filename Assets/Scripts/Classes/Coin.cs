using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public new AudioSource audio;

    private readonly float autoDestructionTime = 3f;
    private bool collected = false;

    private void OnEnable()
    {
        collected = false;
        transform.localScale = new Vector2(38f, 38f);
        StartCoroutine(AutoDestruction());
    }

    private IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(autoDestructionTime);

        gameObject.SetActive(false);
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
        gameObject.SetActive(false);
    }
}
