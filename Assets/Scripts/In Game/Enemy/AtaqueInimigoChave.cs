using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueInimigoChave : MonoBehaviour
{

    float timeBetweenAttacks = 0.5f;
    public int damage = 10;
    Animator eAnim;
    //Animator anim;
    GameObject gManager;
    bool tookDamage = false;
    GameObject player;
    [SerializeField]
    bool playerInRange;
    float timer = 0;
    AudioSource audioSource;
    void Start()
    {
        player = GameObject.Find("June(Clone)");
        eAnim = GetComponent<Animator>();
        gManager = GameObject.FindGameObjectWithTag("GameManager");
        eAnim.SetLayerWeight(1, 0);
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GetComponent<InimigoChave>().playerOnSight)
            {
                if (player.GetComponent<ControleTeclado>().direcaoTeclado.magnitude > 0)
                {
                    if (eAnim.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
                    {
                        eAnim.SetLayerWeight(1, 1);
                        eAnim.SetLayerWeight(2, 0);
                    }
                }
                else
                {
                    if (eAnim.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
                    {
                        eAnim.SetLayerWeight(1, 0);
                        eAnim.SetLayerWeight(2, 1);
                    }
                }

            }
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eAnim.SetLayerWeight(2, 0);
            eAnim.SetLayerWeight(1, 0);
            playerInRange = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<InimigoChave>().ableToWalk)
        {
            if (playerInRange && GetComponent<InimigoChave>().playerOnSight)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }

            if (timer >= timeBetweenAttacks)
            {
                if (player.GetComponent<ControleTeclado>().direcaoTeclado.magnitude > 0)
                {
                    eAnim.SetLayerWeight(1, 1);
                    eAnim.SetLayerWeight(2, 0);
                }
                else
                {
                    eAnim.SetLayerWeight(2, 1);
                    eAnim.SetLayerWeight(1, 0);
                }
                eAnim.SetBool("Attack", true);
                timer = 0f;
            }
            else
            {
                eAnim.SetBool("Attack", false);
            }
            if ((eAnim.GetCurrentAnimatorStateInfo(1).IsName("Attack") && eAnim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.5f) ||
                (eAnim.GetCurrentAnimatorStateInfo(2).IsName("Attack") && eAnim.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.5f))
            {
                if (!tookDamage)
                {
                    gManager.GetComponent<GameManager>().TakeDamage(damage);
                    tookDamage = true;
                    audioSource.PlayOneShot(GetComponent<Audio>().clips[4]);
                }
            }
            else
            {
                tookDamage = false;
            }
        }
    }
}
