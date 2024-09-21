using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammocounter : MonoBehaviour
{
    public playercont player;
    public TMPro.TMP_Text ammotext;
    void Update()
    {
        ammotext.text = "Ammo:" + player.ammo;
    }
}
