using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetcont : MonoBehaviour
{
    public bool washit;
    public MeshRenderer mesh1, mesh2;
    public AudioSource AS;
    public AudioClip AC;
    public void hit()
    {
        if (washit == false)
        {
            mesh1.enabled = true;
            mesh2.enabled = true;
            washit = true;
            AS.PlayOneShot(AC);
        }
    }
}
