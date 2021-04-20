using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePortal : MonoBehaviour
{
    public GameObject _car;

    void Start()
    {
        InvokeRepeating("SpawnCar", 0, Random.Range(15f, 45f));
    }

    public void SpawnCar()
    {
        Vector3 spawnPosition = new Vector3(-3.5f, transform.position.y, transform.position.z);
        GameObject car = Instantiate(_car, spawnPosition, Quaternion.identity);
        car.GetComponent<Car>().StartUp(-1f, true);
    }
}
