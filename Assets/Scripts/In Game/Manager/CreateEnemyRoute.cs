using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyRoute : MonoBehaviour
{
    //SISTEMA DE ROTA DOS INIMIGOS, CADA WAYPOINT É UM PREFAB NO MAPA QUE DETERMINA POR ONDE O INIMIGO PASSA
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
