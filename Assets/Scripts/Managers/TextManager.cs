using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public string propertyName;
    public Text value;

    public void Start()
    {
        PlayerManager.Instance.GetType().GetField(propertyName).SetValue(PlayerManager.Instance, value);
    }
}
