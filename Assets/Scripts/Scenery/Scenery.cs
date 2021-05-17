using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenery : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += Vector3.back * Time.deltaTime * Game.i.speed;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("non tunnel hit player");
            Game.i.inTunnel = false;
        }
    }
}
