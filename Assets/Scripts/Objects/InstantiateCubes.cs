using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    public float maxScale;
    public static int numCubes = 512;
    private GameObject[] sampleCubes = new GameObject[numCubes];
    public Vector3 offset;
    public bool left_to_right;
    public float startSize;

    private float[] initialSizes = new float[numCubes];

    void Start()
    {
        for (int i = 0; i < numCubes; i++)
        {
            GameObject cubeInstance = (GameObject)Instantiate(cubePrefab);
            cubeInstance.transform.position = this.transform.position;
            cubeInstance.transform.parent = this.transform;
            cubeInstance.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, (45f / numCubes) * i, this.transform.eulerAngles.z);
            cubeInstance.transform.position = Vector3.forward * 240;
            int idx = left_to_right ? i : numCubes - 1 - i;
            sampleCubes[idx] = cubeInstance;
            initialSizes[idx] = Random.Range(startSize, startSize + 10);
        }
        this.transform.eulerAngles = offset;
    }

    void Update()
    {
        for (int i = 0; i < numCubes; i++)
        {
            if (sampleCubes != null)
            {
                Vector3 newScale = new Vector3(transform.localScale.x, (AudioPeer.samples[i] * maxScale) + initialSizes[i], transform.localScale.z);

                sampleCubes[i].transform.localScale = newScale;
                sampleCubes[i].transform.position = new Vector3(sampleCubes[i].transform.position.x, transform.position.y + newScale.y / 2, sampleCubes[i].transform.position.z);

            }
        }
    }
}
