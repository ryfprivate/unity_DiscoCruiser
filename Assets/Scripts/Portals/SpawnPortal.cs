using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public static SpawnPortal i;

    public GameObject _parent;
    public GameObject _scenery;
    public GameObject _car;
    public GameObject[] _clouds;

    private List<GameObject> sceneries;

    void Awake()
    {
        i = this;
    }

    void Start()
    {
        sceneries = new List<GameObject>();
        foreach (GameObject scenery in Resources.LoadAll("Sceneries", typeof(GameObject)))
        {
            sceneries.Add(scenery);
        }

        InvokeRepeating("SpawnCar", 0, Random.Range(10f, 30f));
        InvokeRepeating("SpawnClouds", 0, Random.Range(0f, 10f));
    }

    public void SpawnCar()
    {
        Vector3 spawnPosition = new Vector3(4, transform.position.y, transform.position.z);
        GameObject car = Instantiate(_car, spawnPosition, Quaternion.identity, _parent.transform);
        car.GetComponent<Car>().StartUp(Game.i.speed * 2f, false);
    }

    public void SpawnClouds()
    {
        if (_clouds.Length != 0)
        {
            int idx = Random.Range(0, _clouds.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-150f, 150f), Random.Range(60f, 80f), transform.position.z + 100);
            GameObject cloud = Instantiate(_clouds[idx], spawnPosition, Quaternion.identity, _parent.transform);
        }
    }

    public void Spawn()
    {
        int idx = Random.Range(0, sceneries.Count);

        GameObject nextScenery = Instantiate(sceneries[idx], transform.position, Quaternion.identity, _parent.transform);
        Debug.Log(nextScenery);
    }
}
