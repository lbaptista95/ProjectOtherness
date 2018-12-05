using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    Animator anim;
    GameObject player;
    List<GameObject> enemies;
    GameObject attackedEnemy;
    List<GameObject> enemiesOnRange;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("June(Clone)");
        anim = player.GetComponent<Animator>();
        enemies = new List<GameObject>();
        enemiesOnRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Só é possível executar um ataque corpo a corpo quando june não está atirando
        if (anim.runtimeAnimatorController != null && anim.runtimeAnimatorController.name == "JuneComum")
        {
            //Se há algum inimigo próximo a June 
            if (attackedEnemy != null)
            {
                RaycastHit rHit;
                //Se entre June e o inimigo não há uma parede
                if (Physics.Raycast(transform.parent.position, attackedEnemy.transform.position - transform.parent.position, out rHit, GetComponent<SphereCollider>().radius))
                {
                    Debug.DrawRay(transform.parent.position, attackedEnemy.transform.position - transform.parent.position, Color.red);
                    if (rHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        print("INIMIGO PROXIMO");
                        //Se o inimigo está na frente de june
                        if (Vector3.Angle(transform.forward, attackedEnemy.transform.position - transform.position) <= 75)
                        {
                            //Se é um inimigo comum
                            if (attackedEnemy.GetComponent<MoverInimigo>() != null)
                            {
                                //Se o inimigo está parado e distraído
                                if (attackedEnemy.GetComponent<MoverInimigo>().estado == ESTADO_INIMIGO.OCIOSO ||
                                    (attackedEnemy.GetComponent<MoverInimigo>().estado == ESTADO_INIMIGO.ALERTA && 
                                    attackedEnemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") && 
                                    attackedEnemy.GetComponent<Animator>().GetLayerWeight(2) <= 0))
                                {
                                    attackedEnemy.GetComponentInChildren<Text>().text = "Press F to perform stealth kill";
                                    if (Input.GetKeyDown(KeyCode.F))
                                    {
                                        anim.SetBool("Butt", true);
                                    }
                                    else
                                    {
                                        anim.SetBool("Butt", false);
                                    }
                                }
                                else
                                    attackedEnemy.GetComponentInChildren<Text>().text = "";
                            }
                        }
                        else
                        {
                            attackedEnemy.GetComponentInChildren<Text>().text = "";
                        }
                    }
                }
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Butt") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                if (attackedEnemy != null && !attackedEnemy.GetComponent<EnemyHealth>().isDead)
                {
                    attackedEnemy.GetComponentInChildren<Text>().text = "";
                    GetComponentInParent<ControleTeclado>().noise = 5;
                    enemies.Remove(attackedEnemy);
                    attackedEnemy.GetComponent<EnemyHealth>().EnemyTakeDamage(attackedEnemy.GetComponent<EnemyHealth>().enemyCurrentHealth);
                    attackedEnemy = null;
                }
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            attackedEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attackedEnemy != null)
        {
            if (other.gameObject == attackedEnemy)
            {
                attackedEnemy.GetComponentInChildren<Text>().text = "";
                attackedEnemy = null;
            }
        }

    }
}
