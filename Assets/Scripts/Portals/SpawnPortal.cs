using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public static SpawnPortal i;

    public GameObject _scenery;
    public GameObject _car;

    void Awake()
    {
        i = this;
    }

    void Start()
    {
        InvokeRepeating("SpawnCar", 0, Random.Range(10f, 30f));
    }

    public void SpawnCar()
    {
        Vector3 spawnPosition = new Vector3(4, transform.position.y, transform.position.z);
        GameObject car = Instantiate(_car, spawnPosition, Quaternion.identity);
        car.GetComponent<Car>().StartUp(2f, false);
    }

    public void Spawn()
    {
        Debug.Log("spawn new scenery");
        GameObject nextScenery = Instantiate(_scenery, transform.position, Quaternion.identity);
        Debug.Log(nextScenery);
    }
}
