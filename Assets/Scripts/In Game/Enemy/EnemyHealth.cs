using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{

    // Use this for initialization
    GameObject gManager;
    public int enemyStartingHealth = 50;
    public int enemyCurrentHealth;
    //public Slider enemySlider;
    public bool isDead;
    Rigidbody erb;
    GameObject player;
    Animator anim;
    float destroyTimer;
    AudioSource audioSource;
    void Awake()
    {

        anim = GetComponent<Animator>();
        erb = GetComponent<Rigidbody>();
        gManager = GameObject.FindGameObjectWithTag("GameManager");
        enemyCurrentHealth = enemyStartingHealth;
        audioSource = GetComponent<AudioSource>();
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {            
            Destroy(this.gameObject, 3);
        }
    }

    public void EnemyTakeDamage(int amount)
    {
        audioSource.PlayOneShot(GetComponent<Audio>().RandomizeEnemyDamageSound());
        enemyCurrentHealth -= amount;
        if (enemyCurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        anim.enabled = false;
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }

        if (GameObject.Find("June(Clone)").GetComponent<ControleTeclado>().isGiraffe)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().deaths++;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().deathTimer = 0;            
        }


        isDead = true;
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shoot"))
        {
            EnemyTakeDamage(10);
        }
    }

}
