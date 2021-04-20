using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateWaves : MonoBehaviour
{
    public GameObject cubePrefab;
    public float maxScale;
    public static int numCubes = 512;
    private int num;
    private GameObject[] sampleCubes = new GameObject[numCubes * 4];


    void Start()
    {
        int size = 22;
        num = 0;
        for (int i = 0; i < 22 * 22; i++)
        {
            GameObject cubeInstance = (GameObject)Instantiate(cubePrefab);
            cubeInstance.transform.parent = this.transform;

            float length = cubeInstance.transform.localScale.x;
            float width = cubeInstance.transform.localScale.z;
            int x = i % size;
            int y = i / size;
            cubeInstance.transform.position = new Vector3(transform.position.x + x * length, transform.position.y, transform.position.z + y * width);
            // Create a mirrored cubes on each axis
            GameObject cubeInstance2 = (GameObject)Instantiate(cubePrefab);
            cubeInstance2.transform.parent = this.transform;
            cubeInstance2.transform.position = new Vector3(transform.position.x - x * length, transform.position.y, transform.position.z + y * width);

            GameObject cubeInstance3 = (GameObject)Instantiate(cubePrefab);
            cubeInstance3.transform.parent = this.transform;
            cubeInstance3.transform.position = new Vector3(transform.position.x + x * length, transform.position.y, transform.position.z - y * width);

            GameObject cubeInstance4 = (GameObject)Instantiate(cubePrefab);
            cubeInstance4.transform.parent = this.transform;
            cubeInstance4.transform.position = new Vector3(transform.position.x - x * length, transform.position.y, transform.position.z - y * width);

            sampleCubes[num] = cubeInstance;
            num++;
            sampleCubes[num] = cubeInstance2;
            num++;
            sampleCubes[num] = cubeInstance3;
            num++;
            sampleCubes[num] = cubeInstance4;
            num++;
        }
    }

    void Update()
    {
        int idx = 0;
        for (int i = 0; i < num; i++)
        {
            if (sampleCubes != null)
            {
                if (idx >= numCubes) { idx = 0; }
                Vector3 newScale = new Vector3(sampleCubes[idx].transform.localScale.x, (AudioPeer.samples[idx] * maxScale) + 1, sampleCubes[idx].transform.localScale.z);

                sampleCubes[idx].transform.localScale = newScale;
                sampleCubes[idx].transform.position = new Vector3(sampleCubes[idx].transform.position.x, -10 + newScale.y / 2, sampleCubes[idx].transform.position.z);
                idx++;
            }
        }
    }
}
