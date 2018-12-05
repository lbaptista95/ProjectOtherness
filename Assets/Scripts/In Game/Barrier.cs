using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    bool startCount;
    bool countUp;
    float upTime;
    float downTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (startCount && !countUp)
        {
            print("TEM Q DESCER1");
            if (downTime >= 5)
            {
                print("TEM Q DESCER2");
                print(transform.localPosition.y + "> -10.5f = " + (transform.localPosition.y > -10.5f) );
                if (transform.localPosition.y > -10.5f)
                {
                    print("TEM Q DESCER3");
                    Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                    newPos.y -= 0.2f;
                    transform.localPosition = newPos;
                    
                }
            }
            else
                downTime += Time.deltaTime;
        }
        else if (!startCount && countUp)
        {            
            if (upTime>=3)
            {
                if (transform.localPosition.y < -5f)
                {
                    Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                    newPos.y += 0.2f;
                    transform.localPosition = newPos;
                }
            }
            else
                upTime += Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startCount = true;
            countUp = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startCount = true;
            countUp = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countUp = true;
            startCount = false;
        }
    }
}
