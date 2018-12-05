using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyRoute : MonoBehaviour
{
    public string enemyTypeName;
    public GameObject[] waypoints;
    public GameObject enemyType;
    // Use this for initialization
    void Awake()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false; 
        for (int x = 0; x < waypoints.Length; x++)
        {
            waypoints[x].GetComponent<Renderer>().enabled = false;
        }
        enemyTypeName = enemyType.name;        
    }    

    void Update()
    {
        
    }
}
