using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    public float maxScale;
    public static int numCubes = 512;
    private GameObject[] sampleCubes = new GameObject[numCubes];

    void Start()
    {
        for (int i = 0; i < numCubes; i++)
        {
            GameObject cubeInstance = (GameObject)Instantiate(cubePrefab);
            cubeInstance.transform.position = this.transform.position;
            cubeInstance.transform.parent = this.transform;
            cubeInstance.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -(360f / numCubes) * i, 0);
            cubeInstance.transform.position = Vector3.forward * 40;
            sampleCubes[i] = cubeInstance;
        }
    }

    void Update()
    {
        for (int i = 0; i < numCubes; i++)
        {
            if (sampleCubes != null)
            {
                Vector3 newScale = new Vector3(transform.localScale.x, (AudioPeer.samples[i] * maxScale), transform.localScale.z);

                sampleCubes[i].transform.localScale = newScale;
                sampleCubes[i].transform.position = new Vector3(sampleCubes[i].transform.position.x, 0 + newScale.y / 2, sampleCubes[i].transform.position.z);

            }
        }
    }
}
