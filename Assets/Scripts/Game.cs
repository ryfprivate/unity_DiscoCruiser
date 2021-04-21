using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game i;
    public bool inTunnel;
    public float speed;

    void Start()
    {
        inTunnel = false;
        speed = 3f;
    }

    void Awake()
    {
        i = this;
    }
}
