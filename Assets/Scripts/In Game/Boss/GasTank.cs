using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour
{

    // Use this for initialization
    public GameObject fog;
    public GameObject fogInstance;
    GameObject boss;
    float fogTimer;
    GameObject[] tanks;
    void Start()
    {          
        boss = GameObject.FindGameObjectWithTag("Boss1");       
    }

    // Update is called once per frame
    void Update()
    {
        tanks = GameObject.FindGameObjectsWithTag("GasTank");        
        if (fogInstance != null)
        {            
            fogTimer += Time.deltaTime;

            if (fogTimer >= 10)
            {
                fogTimer = 0;
                boss.GetComponent<Boss1>().fumaca = false;                
                Destroy(fogInstance);
                for (int x = 0; x < tanks.Length; x++)
                {
                    if (tanks[x] != gameObject)
                    {
                        tanks[x].GetComponent<GasTank>().enabled = true;
                        tanks[x].GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        print("kgrjigjreuig");
        if (other.CompareTag("Shoot"))
        {
            for (int x=0;x<tanks.Length;x++)
            {
                if (tanks[x]!=gameObject)
                {
                    tanks[x].GetComponent<GasTank>().enabled = false;
                    tanks[x].GetComponent<Collider>().enabled = false;
                }
            }
            boss.GetComponent<Boss1>().fumaca = true;
            GetComponentInChildren<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            fogInstance = Instantiate(fog);
            fogInstance.SetActive(true);
        }
    }
}
