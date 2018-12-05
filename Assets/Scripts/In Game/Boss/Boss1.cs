using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOSS_STATE { CEGO, VENDO, CAIDO }
public class Boss1 : MonoBehaviour
{
    public ParticleSystem bossShot;
    GameObject angle0;
    Vector3 angle0Pos;
    GameObject angle1;
    Vector3 angle1Pos;
    GameObject crosshair0;
    GameObject crosshair1;
    Quaternion crosshairRotation;
    Quaternion ch0Rotation;
    Quaternion ch1Rotation;
    Quaternion bossRotation;
    Quaternion crosshairOrigin;
    public BOSS_STATE bossState;
    float shotTime;
    float waitTime;
    public bool fumaca;
    public float sightAngle = 200f;
    float playerAngle;
    Vector3 playerDirection;
    public GameObject player;
    bool playerOnSight;
    public GameObject crossHair;
    Quaternion originRotation;
    float rotationTime;
    float rotationTime2;
    Animator bossAnim;
    GameObject bossSpine;
    GameObject crossHair2;
    bool sentido;
    AudioSource aSource;
    // Use this for initialization
    void Start()
    {
        bossAnim = GetComponent<Animator>();
        crossHair2 = GameObject.Find("CrossHair2");
        crossHair = GameObject.Find("CrossHair");
        player = GameObject.Find("June(Clone)");
        waitTime = 0;
        angle0 = GameObject.Find("Angle0");
        angle0Pos = angle0.transform.position;
        angle1 = GameObject.Find("Angle1");
        angle1Pos = angle1.transform.position;
        crosshair0 = GameObject.Find("Crosshair0");
        crosshair1 = GameObject.Find("Crosshair1");
        crosshair0.transform.LookAt(angle0Pos);
        crosshair1.transform.LookAt(angle1Pos);
        ch0Rotation = crosshair0.transform.rotation;
        ch1Rotation = crosshair1.transform.rotation;
        crosshairRotation = ch1Rotation;
        rotationTime2 = 0;
        bossState = BOSS_STATE.VENDO;
        bossSpine = GameObject.FindGameObjectWithTag("BossColuna");
        crossHair2.transform.LookAt(angle0Pos);
        sentido = false;
        bossAnim.SetLayerWeight(1, 0);
        bossAnim.SetBool("Falling", false);
        aSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (playerOnSight || bossState == BOSS_STATE.CEGO)
        {
            bossAnim.SetBool("Shooting", true);
        }
        else
        {
            bossAnim.SetBool("Shooting", false);
        }
    }
    private void LateUpdate()
    {       
        shotTime += Time.fixedDeltaTime;
        switch (bossState)
        {
            case BOSS_STATE.VENDO:
                playerDirection = player.transform.position - transform.position;
                playerAngle = Vector3.Angle(playerDirection, transform.forward);
                if (playerAngle <= sightAngle / 2)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.forward + transform.up, playerDirection.normalized, out hit, 500))
                    {
                        Debug.DrawRay(transform.position + transform.up + transform.forward, playerDirection.normalized, Color.red);
                        if (hit.collider.gameObject == player)
                        {
                            playerOnSight = true;
                        }
                        else
                        {
                            originRotation = bossSpine.transform.rotation;
                            playerOnSight = false;
                        }
                    }
                }
                if (playerOnSight)
                {
                    crossHair.transform.LookAt(player.transform.position);

                    bossSpine.transform.localRotation = Quaternion.Euler(0, crossHair.transform.rotation.eulerAngles.y + 95, -90);

                    rotationTime2 += 5;
                    Quaternion from = bossSpine.transform.rotation;
                    if (shotTime > 0.1f)
                    {
                        aSource.PlayOneShot(aSource.clip);
                        bossShot.Emit(3);
                        shotTime = 0;
                    }
                }
                else
                {

                }
                if (fumaca)
                    bossState = BOSS_STATE.CEGO;
                break;
            case BOSS_STATE.CEGO:
                {
                    if (sentido)
                        crossHair2.transform.rotation = Quaternion.Lerp(crossHair2.transform.rotation, ch0Rotation, Time.deltaTime);
                    else
                        crossHair2.transform.rotation = Quaternion.Lerp(crossHair2.transform.rotation, ch1Rotation, Time.deltaTime);

                    if (Vector3.Angle(crossHair2.transform.forward,crosshair0.transform.forward)<=3)
                        sentido = false;
                    else if (Vector3.Angle(crossHair2.transform.forward, crosshair1.transform.forward) <= 3)
                        sentido = true;
                    
                    bossSpine.transform.localRotation = Quaternion.Euler(0, crossHair2.transform.rotation.eulerAngles.y + 95, -90);

                    if (shotTime > 0.1f)
                    {
                        aSource.PlayOneShot(aSource.clip);
                        bossShot.Emit(5);
                        shotTime = 0;
                    }
                    if (!fumaca)
                    {
                        bossState = BOSS_STATE.VENDO;
                    }
                    break;
                }           
        }
    }


}
