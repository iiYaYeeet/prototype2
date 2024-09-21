using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class infammo : MonoBehaviour
{
    public SpriteRenderer SR;
    public BoxCollider BC;
    public ParticleSystem PS;
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<playercont>();
            player.infammmo = true;
            SR.enabled = false;
            BC.enabled = false;
            PS.Emit(20);
        }
    }
}
