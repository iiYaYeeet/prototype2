using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covercontroller : MonoBehaviour
{
    public MeshRenderer mesh1;
    public BoxCollider BC, bc2;
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            mesh1.enabled = true;
            BC.enabled = true;
            bc2.enabled = false;
        }
    }
}
