using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{

    // Use this for initialization
    GameManager gManager;
    public int bossStartingHealth = 300;
    public int bossCurrentHealth;
    public Slider bossSlider;
    public bool isDead;
    public GameObject bossDialogue;
    public Boss1 bossBehavior;
    Animator bossAnim;
    GameObject player;
    public bool defeated;

    void Awake()
    {
        bossAnim = GetComponent<Animator>();
        gManager = GameManager.FindObjectOfType<GameManager>();        
        bossBehavior = GetComponent<Boss1>();
        bossCurrentHealth = bossStartingHealth;
        bossAnim.SetLayerWeight(1, 0);
        GetComponent<Dialogue>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bossSlider.value = bossCurrentHealth;
        if (bossCurrentHealth <= 10)
        {
            defeated = true;
            
        }
        if (defeated)
        {
            
            print("ERA PRA CAIR");
            bossBehavior.enabled = false;
            bossAnim.SetBool("Shooting", false);
            bossAnim.SetLayerWeight(0, 0);
            bossAnim.SetLayerWeight(1, 1);
            bossAnim.SetBool("Falling", true);           
            bossBehavior.enabled = false;
            Cursor.visible = true;
            GetComponent<Dialogue>().enabled = true;
        }
    }

    public void bossTakeDamage(int amount)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossCurrentHealth -= amount;        
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shoot"))
        {
            if (bossCurrentHealth > 10)
                bossTakeDamage(10);
        }
    }

}
