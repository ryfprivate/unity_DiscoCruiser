using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed = 0;
    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5f, 10f);
    }

    void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("collission" + other.gameObject.tag);
        if (other.gameObject.tag == "DeletePortal")
        {
            if (!spawned)
            {

                spawned = true;
                return;
            }

            Destroy(gameObject);
        }

        if (other.gameObject.tag == "SpawnPortal")
        {
            if (!spawned)
            {

                spawned = true;
                return;
            }

            Destroy(gameObject);
        }
    }
}
