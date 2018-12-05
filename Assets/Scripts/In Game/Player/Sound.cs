using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sound : MonoBehaviour
{

    GameObject player;
    List<GameObject> enemies;
    Vector3 enemyDirection;
    NavMeshAgent playerAgent;
    bool bateu;
    RaycastHit soundHit;
    // Use this for initialization
    void Start()
    {
        enemies = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("June(Clone)");
        playerAgent = player.GetComponent<NavMeshAgent>(); ;
        if (enemies.Count <= 0)
        {
            bateu = false;
        }
        else if (bateu)
        {
            for (int x = 0; x < enemies.Count; x++)
            {
                if (enemies[x] != null)
                {
                    enemyDirection = enemies[x].transform.position - transform.position;
                    if (Physics.Raycast(transform.position, enemyDirection.normalized, out soundHit, this.GetComponent<SphereCollider>().radius, ~(1 << 9)))
                    {
                        if (soundHit.collider.gameObject == enemies[x])
                        {
                            enemies[x].GetComponent<MoverInimigo>().estado = ESTADO_INIMIGO.ALERTA;
                            enemies[x].GetComponent<MoverInimigo>().shootSeeker.transform.LookAt(transform.position);
                            float compareAngle = Vector3.SignedAngle(transform.forward, enemies[x].transform.position - transform.position, Vector3.up);
                            enemies[x].GetComponent<MoverInimigo>().enemyAnim.SetFloat("Turn", compareAngle);
                            enemies[x].transform.rotation = Quaternion.Lerp(enemies[x].transform.rotation, enemies[x].GetComponent<MoverInimigo>().shootSeeker.transform.rotation, Time.deltaTime);
                        }
                    }
                }
            }

        }
        if (player.GetComponent<ControleTeclado>().noise >= 4.0f && player.GetComponent<ControleTeclado>().noise < 7.0f)
        {
            this.GetComponent<SphereCollider>().radius = 8.0f;
        }
        else if (player.GetComponent<ControleTeclado>().noise <= 2)
        {
            this.GetComponent<SphereCollider>().radius = 0.7f;
        }
        else
        {
            this.GetComponent<SphereCollider>().radius = 4.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<MoverInimigo>() != null)
                if (other.gameObject.GetComponent<MoverInimigo>().ableToWalk)
                {
                    bateu = true;
                    enemies.Add(other.gameObject);
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            for (int x = 0; x < enemies.Count; x++)
            {
                if (enemies[x] == other.gameObject)
                {
                    enemies.Remove(other.gameObject);
                }
            }
        }
    }
}
