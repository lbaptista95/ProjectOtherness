using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingWet : MonoBehaviour
{

    // Use this for initialization
    Material[] juneMaterials;
    float wetPercent;
    public Material juneWet;

    void Start()
    {
        juneMaterials = new Material[GetComponentsInChildren<Renderer>().Length];
        for (int x = 0; x < GetComponentsInChildren<Renderer>().Length; x++)
        {
            juneMaterials[x] = GetComponentsInChildren<Renderer>()[x].material;
        }
        wetPercent = 0;
        juneWet = Resources.Load<Material>("JuneWet");
    }

    // Update is called once per frame
    void Update()
    {
        //Molha June de acordo com a quantidade de chuva que ela vai tomando
        for (int x = 0; x < juneMaterials.Length; x++)
        {
            if (juneMaterials[x].name == "JuneWet (Instance)")
            {
                if (juneMaterials[x].GetFloat("_Water") <= 1)
                    juneMaterials[x].SetFloat("_Water", wetPercent);
                if (juneMaterials[x].GetFloat("_Glossiness") <= 0.9f)
                    juneMaterials[x].SetFloat("_Glossiness", wetPercent);
            }
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Rain"))
        {
            wetPercent += 0.05f;
        }
    }
}
