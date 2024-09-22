using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammocounter : MonoBehaviour
{
    public playercont player;
    public timecontroller TC;
    public TMPro.TMP_Text ammotext,timetext;
    void Update()
    {
        ammotext.text = "Ammo:" + player.ammo;
        timetext.text = "Time:" +
                        "" + TC.watch.Elapsed;
    }
}
