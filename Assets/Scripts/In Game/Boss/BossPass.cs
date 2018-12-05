using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPass : MonoBehaviour
{

    // Use this for initialization
    AlternarControles playerControl;
    void Start()
    {
        playerControl = GameObject.Find("June(Clone)").GetComponent<AlternarControles>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.interact)
        {
            SceneManager.LoadScene("Cutscene - Final");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "June(Clone)")
        {
            playerControl.InteractionAvailable("hand him the letter");
        }
    }
}
