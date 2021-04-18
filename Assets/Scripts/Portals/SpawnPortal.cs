using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public static SpawnPortal i;

    public GameObject _scenery;

    void Awake()
    {
        i = this;
    }

    void Start()
    {

    }

    public void Spawn()
    {
        Debug.Log("spawn new scenery");
        GameObject nextScenery = Instantiate(_scenery, transform.position, Quaternion.identity);
        Debug.Log(nextScenery);
    }
}
