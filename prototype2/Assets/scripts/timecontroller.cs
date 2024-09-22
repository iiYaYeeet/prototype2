using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class timecontroller : MonoBehaviour
{
    public Stopwatch watch;

    public void Start()
    {
        watch = new Stopwatch();
        watch.Start();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            watch.Stop();
        }
    }
}
