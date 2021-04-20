using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("collission" + other.gameObject.tag);
        if (other.gameObject.tag == "DeletePortal")
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        if (other.gameObject.tag == "SpawnPortal")
        {
            SpawnPortal.i.Spawn();
        }
    }
}
