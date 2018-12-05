using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimOnKeyUp : MonoBehaviour {

    public GameObject mainProjectile;
    public ParticleSystem mainParticleSystem;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mainProjectile.SetActive(true);
        }

        if (mainParticleSystem.IsAlive() == false)
            mainProjectile.SetActive(false);
		
	}
}
