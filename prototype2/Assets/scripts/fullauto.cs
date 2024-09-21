using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullauto : MonoBehaviour
{
    public SpriteRenderer SR;
    public BoxCollider BC;
    public ParticleSystem PS;
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<playercont>();
            player.fullautoupgrade = true;
            SR.enabled = false;
            BC.enabled = false;
            PS.Emit(20);
        }
    }
}
