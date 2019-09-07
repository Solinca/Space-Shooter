using UnityEngine;

public class ResizeBackgroundToScreen : MonoBehaviour
{
    private void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();

        transform.localScale = Vector3.one;

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height - 3);
    }
}
