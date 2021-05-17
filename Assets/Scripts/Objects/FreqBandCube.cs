using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreqBandCube : MonoBehaviour
{
    public int band;
    public float startSize = 0, maxScale = 5;
    public bool useBuffer;
    public Material material;

    private Vector3 initialPosition;

    void Start()
    {
        // material = GetComponent<MeshRenderer>().materials[0];
        initialPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newScale;
        float scaleAmount = 0;
        Color color;
        if (useBuffer)
        {
            scaleAmount = AudioPeer.audioBandBuffer[band];
        }
        else
        {
            scaleAmount = AudioPeer.audioBand[band];
        }

        if (scaleAmount > 0)
        {

            color = new Color(scaleAmount / 2, scaleAmount / 2, scaleAmount / 2);
            // material.SetColor("_EmissionColor", color);

            newScale = new Vector3(transform.localScale.x, (scaleAmount * maxScale) + startSize, transform.localScale.z);
            transform.localScale = newScale;
            transform.position = new Vector3(transform.position.x, initialPosition.y - .5f + newScale.y / 2, transform.position.z);
        }

    }
}
