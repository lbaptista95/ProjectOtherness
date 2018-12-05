using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueInimigo : MonoBehaviour
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
            if (GetComponent<MoverInimigo>().playerOnSight)
            {
                if (player.GetComponent<ControleTeclado>().direcaoTeclado.magnitude > 0)
                {
                    if (eAnim.GetCurrentAnimatorStateInfo(1).IsName("Punch"))
                    {
                        eAnim.SetLayerWeight(1, 1);
                        eAnim.SetLayerWeight(2, 0);
                    }
                }
                else
                {
                    if (eAnim.GetCurrentAnimatorStateInfo(2).IsName("Punch"))
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
        if (!gManager.GetComponent<GameManager>().isDead)
        {
            if (GetComponent<MoverInimigo>().ableToWalk)
            {
                if (playerInRange && GetComponent<MoverInimigo>().playerOnSight)
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
                    eAnim.SetBool("Punch", true);
                    print("ATACA");
                    timer = 0f;
                }
                else
                {
                    eAnim.SetBool("Punch", false);
                }
                if ((eAnim.GetCurrentAnimatorStateInfo(1).IsName("Punch") && eAnim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.5f) ||
                    (eAnim.GetCurrentAnimatorStateInfo(2).IsName("Punch") && eAnim.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.5f) ||
                    (eAnim.GetCurrentAnimatorStateInfo(2).IsName("Kick") && eAnim.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.3f))
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
}
