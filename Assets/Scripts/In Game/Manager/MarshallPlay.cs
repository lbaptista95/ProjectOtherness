using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshallPlay : MonoBehaviour
{
    //ALTERA O VOLUME DO ÁUDIO DO DISCURSO DE MARSHALL DE ACORDO COM A DISTÂNCIA DE JUNE
    // Use this for initialization
    bool hearing;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hearing)
        {
            GetComponent<AudioSource>().volume = 1 / ((Vector3.Distance(GameObject.Find("June(Clone)").transform.position, transform.position) / 4));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hearing = true;
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hearing = false;
            GetComponent<AudioSource>().Stop();
        }
    }
}
