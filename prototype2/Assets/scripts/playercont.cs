using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Debug = System.Diagnostics.Debug;

public class playercont : MonoBehaviour
{
    //comps
    public Rigidbody RB;
    public LineRenderer laserLine;
    public ParticleSystem gunps;
    public AudioSource AS;
    public AudioClip reloadclip;
    public AudioClip Shoot;
    //floats
    public float MouseSensitivity = 3;
    public float WalkSpeed = 10;
    public float ammo = 30;
    //upgradebools
    public bool fullautoupgrade;
    public bool infammmo;
    //otherbools
    public bool canshoot = true;
    //headbob stuff
    public float transitionSpeed = 10f;
    public float bobSpeed = 4.8f;
    public float bobAmount = 0.05f;
    float timer = Mathf.PI / 2;
    //hitscan stuff
    public float fireRate = 0.15f;
    public float nextFire;
    public WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    public int gunDamage = 1;
    public Transform gunEnd;
    public float weaponRange = 50f; 
    //objs
    public Camera Eyes;
    public Vector3 camPos;
    public Vector3 restPosition;
    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
            //movement
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D)) //moving
            {
                timer += bobSpeed * Time.deltaTime;
                Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount,
                    restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount)),
                    restPosition.z); //abs val of y for a parabolic path
                camPos = newPosition;
            }

            else
            {
                timer = Mathf.PI / 2;

                Vector3 newPosition = new Vector3(
                    Mathf.Lerp(camPos.x, restPosition.x, transitionSpeed * Time.deltaTime),
                    Mathf.Lerp(camPos.y, restPosition.y, transitionSpeed * Time.deltaTime),
                    Mathf.Lerp(camPos.z, restPosition.z,
                        transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
                camPos = newPosition;
            }

            Eyes.transform.localPosition = camPos;
            if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
                timer = 0;
            //get mousexy
            float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
            float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
            //horrot
            transform.Rotate(0, xRot, 0);
            //get rot
            Vector3 Prot = Eyes.transform.localRotation.eulerAngles;
            //add change to rot
            Prot.x += yRot;
            //if's
            if (Prot.x < -180)
            {
                Prot.x += 360;
            }

            if (Prot.x > 180)
            {
                Prot.x -= 360;
            }

            //clamp minmax
            Prot = new Vector3(Mathf.Clamp(Prot.x, -65, 40), 0, 0);
            //plug back in
            Eyes.transform.localRotation = Quaternion.Euler(Prot);

            if (WalkSpeed > 0)
            {
                //set 0
                Vector3 move = Vector3.zero;
                //fore
                if (Input.GetKey(KeyCode.W))
                    move += transform.forward;
                //aft
                if (Input.GetKey(KeyCode.S))
                    move -= transform.forward;
                //left
                if (Input.GetKey(KeyCode.A))
                    move -= transform.right;
                //right
                if (Input.GetKey(KeyCode.D))
                    move += transform.right;
                //setspeed
                move = move.normalized * WalkSpeed;
                //plug back in
                RB.velocity = move;
            }

            if (Input.GetMouseButtonDown(0) && Time.time > nextFire && ammo >= 0 && canshoot)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine (ShotEffect());
                Vector3 rayOrigin = Eyes.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                gunps.Emit(5);
                laserLine.SetPosition (0, gunEnd.position);
                if (!infammmo)
                {
                    ammo--;
                }
                //check if hit anything
                if (Physics.Raycast (rayOrigin, Eyes.transform.forward, out hit, weaponRange))
                {
                    laserLine.SetPosition (1, hit.point);
                    targetcont targ = hit.collider.GetComponent<targetcont>();
                    if (targ != null)
                    {
                        targ.hit();
                    }
                }
                else
                {
                    laserLine.SetPosition (1, rayOrigin + (Eyes.transform.forward * weaponRange));
                }
            }
            if (Input.GetMouseButton(0) && Time.time > nextFire && ammo >= 0 && fullautoupgrade && canshoot)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine (ShotEffect());
                Vector3 rayOrigin = Eyes.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                gunps.Emit(5);
                laserLine.SetPosition (0, gunEnd.position);
                ammo--;

                //check if hit anything
                if (Physics.Raycast (rayOrigin, Eyes.transform.forward, out hit, weaponRange))
                {
                    laserLine.SetPosition (1, hit.point);
                    targetcont targ = hit.collider.GetComponent<targetcont>();
                    if (targ != null)
                    {
                        targ.hit();
                    }
                }
                else
                {
                    laserLine.SetPosition (1, rayOrigin + (Eyes.transform.forward * weaponRange));
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(reload());
            }
            
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                StartCoroutine(reload());
            }
    }

    public IEnumerator ShotEffect()
    {
        AS.PlayOneShot(Shoot);
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    public IEnumerator reload()
    {
        AS.PlayOneShot(reloadclip);
        canshoot = false;
        yield return new WaitForSeconds(2);
        canshoot = true;
        ammo = 30;
    }
}

