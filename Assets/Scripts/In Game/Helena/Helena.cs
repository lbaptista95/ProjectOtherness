using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cradle;

public class Helena : MonoBehaviour
{

    // Use this for initialization
    Animator helenaAnim;
    NavMeshAgent helenaAgent;
    GameObject[] spots;
    GameObject spot;
    Vector3 destination;
    bool isOnSpot;
    float spotTimer;
    GameObject room;
    GameObject june;
    void Start()
    {
        room = GameObject.Find("HelenaRoom");
        helenaAnim = GetComponent<Animator>();
        helenaAgent = GetComponent<NavMeshAgent>();
        spots = GameObject.FindGameObjectsWithTag("HelenaSpot");
        spot = spots[0];
        isOnSpot = false;
        june = GameObject.Find("June(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(june.transform.position, room.transform.position) <= 7)
        {
            if (Vector3.Distance(june.transform.position,transform.position)<=2)
            {
                helenaAgent.isStopped = true;
            }
            else
            {
                helenaAgent.isStopped = false;
                helenaAgent.SetDestination(june.transform.position);
            }
        }
        else
        {
            for (int x = 0; x < spots.Length; x++)
            {
                if (transform.position.x == spots[x].transform.position.x && transform.position.z == transform.position.z)
                {
                    spot = spots[x];
                    helenaAgent.isStopped = true;
                    isOnSpot = true;
                }
            }
            if (isOnSpot)
            {
                spotTimer += Time.deltaTime;
            }
            else
            {
                spotTimer = 0;
            }
            if (spotTimer >= 17)
            {
                helenaAgent.isStopped = false;
                for (int x = 0; x < spots.Length; x++)
                {
                    if (spot == spots[x])
                    {
                        if (x == 3)
                            helenaAgent.SetDestination(spots[0].transform.position);
                        else
                            helenaAgent.SetDestination(spots[x + 1].transform.position);
                    }
                }
                isOnSpot = false;
                spotTimer = 0;
            }
        }
        if (helenaAgent.isStopped)
            helenaAnim.SetBool("Walk", false);
        else
            helenaAnim.SetBool("Walk", true);
    }
}
