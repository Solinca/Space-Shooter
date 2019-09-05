using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public List<GameObject> list;
    public GameObject prefab;
    public string key;
    public int size;
}