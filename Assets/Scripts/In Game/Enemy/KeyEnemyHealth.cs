using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class KeyEnemyHealth : MonoBehaviour
{

    // Use this for initialization
    GameManager gManager;
    public float enemyStartingHealth = 100;
    public float enemyCurrentHealth;
    public int timesAttacked = 0;
    //public Slider enemySlider;
    public bool isDead;
    Rigidbody erb;
    GameObject player;
    Animator anim;
    float destroyTimer;
    AudioSource audioSource;
    Slider healthSlider;
    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
        erb = GetComponent<Rigidbody>();
        gManager = GameManager.FindObjectOfType<GameManager>();
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
        healthSlider.value = enemyCurrentHealth;
        if(GetComponent<InimigoChave>().estado==ESTADO_INIMIGOCHAVE.DEFENDENDO && enemyCurrentHealth<=enemyStartingHealth)
        {
            enemyCurrentHealth+=0.1f;
        }
        if (enemyCurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void EnemyTakeDamage(int amount)
    {
        audioSource.PlayOneShot(GetComponent<Audio>().RandomizeEnemyDamageSound());
        enemyCurrentHealth -= amount;
        timesAttacked++;        
    }

    public void Death()
    {
        anim.SetFloat("Velocity", 0);
        isDead = true;
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shoot"))
        {
            if (GetComponent<InimigoChave>().ableToWalk)
                EnemyTakeDamage(5);
            else
                EnemyTakeDamage(10);
        }
    }

}


