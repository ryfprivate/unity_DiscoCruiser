using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public static SpawnPortal i;

    public GameObject _scenery;
    public GameObject _car;
    public GameObject[] _clouds;

    void Awake()
    {
        i = this;
    }

    void Start()
    {
        InvokeRepeating("SpawnCar", 0, Random.Range(10f, 30f));
        InvokeRepeating("SpawnClouds", 0, Random.Range(0f, 3f));
    }

    public void SpawnCar()
    {
        Vector3 spawnPosition = new Vector3(4, transform.position.y, transform.position.z);
        GameObject car = Instantiate(_car, spawnPosition, Quaternion.identity);
        car.GetComponent<Car>().StartUp(10f, false);
    }

    public void SpawnClouds()
    {
        if (_clouds.Length != 0)
        {
            int idx = Random.Range(0, _clouds.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-150f, 150f), Random.Range(40f, 60f), transform.position.z + 100);
            GameObject cloud = Instantiate(_clouds[idx], spawnPosition, Quaternion.identity);
        }
    }

    public void Spawn()
    {
        Debug.Log("spawn new scenery");
        GameObject nextScenery = Instantiate(_scenery, transform.position, Quaternion.identity);
        Debug.Log(nextScenery);
    }
}
