using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : Scenery
{
    public GameObject _mountain;
    public bool isActive = true;

    void Update()
    {
        isActive = !Game.i.inTunnel;
        if (Game.i.inTunnel)
        {
            _mountain.SetActive(false);
        }
        else
        {
            _mountain.SetActive(true);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("tunnel hit player");
            Game.i.inTunnel = true;
            _mountain.SetActive(false);
        }
    }
}
