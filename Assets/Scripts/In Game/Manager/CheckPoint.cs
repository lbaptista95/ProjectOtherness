using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class CheckPoint : MonoBehaviour
{

    GameObject player;
    GameObject[] enemies;
    Quaternion[] enemiesRotation;
    Quaternion[] enemiesCanvasRotation;
    public string[] enemiesInfo;
    public string sceneName;
    public string[] enemyTypes;
    GameObject gManager;
    AlternarControles playerControl;
    GameObject savingTxt;
    // Use this for initialization
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
        savingTxt = GameObject.Find("SavingText");
        savingTxt.GetComponent<Text>().text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("June(Clone)"))
        {
            gManager = GameObject.FindGameObjectWithTag("GameManager");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesCanvasRotation = new Quaternion[enemies.Length];
            enemiesRotation = new Quaternion[enemies.Length];
            enemiesInfo = new string[enemies.Length];
            enemyTypes = new string[enemies.Length];
            for (int x = 0; x < enemies.Length; x++)
            {
                if (enemies[x].GetComponent<MoverInimigo>() != null)
                {
                    enemiesInfo[x] = enemies[x].GetComponent<MoverInimigo>().enemyRouteInfo;
                    enemiesRotation[x] = enemies[x].transform.rotation;
                    enemyTypes[x] = enemies[x].GetComponent<MoverInimigo>().eTypeName;
                    enemiesCanvasRotation[x] = enemies[x].GetComponentInChildren<NonRotateHealthBar>().transform.rotation;
                }
                else if (enemies[x].GetComponent<InimigoChave>() != null)
                {
                    PlayerPrefs.SetFloat("EnemyLife", enemies[x].GetComponent<KeyEnemyHealth>().enemyCurrentHealth);
                    enemiesInfo[x] = enemies[x].GetComponent<InimigoChave>().enemyRouteInfo;
                    enemiesRotation[x] = enemies[x].transform.rotation;
                    enemyTypes[x] = enemies[x].GetComponent<InimigoChave>().eTypeName;
                    enemiesCanvasRotation[x] = enemies[x].GetComponentInChildren<NonRotateHealthBar>().transform.rotation;
                }
            }
            sceneName = SceneManager.GetActiveScene().name;
            player = other.gameObject;
            PlayerPrefsX.SetVector3("Personagem", player.transform.position);
            PlayerPrefsX.SetBool("Girafa", player.GetComponent<ControleTeclado>().isGiraffe);            
            PlayerPrefsX.SetStringArray("Inimigos", enemiesInfo);            
            PlayerPrefsX.SetStringArray("TiposDeInimigo", enemyTypes);
            PlayerPrefsX.SetQuaternionArray("Canvas", enemiesCanvasRotation);
            PlayerPrefs.SetString("Cena", sceneName);
            if (enemiesRotation != null)
                PlayerPrefsX.SetQuaternionArray("RotacaoInimigos", enemiesRotation);
            PlayerPrefs.SetInt("PlayerHealth", gManager.GetComponent<GameManager>().currentHealth);
            PlayerPrefs.SetString("Mission", GameObject.Find("Quest").GetComponent<Text>().text);
            PlayerPrefsX.SetBool("MissionBool", player.GetComponentInParent<AlternarControles>().mission);
            if (GameObject.Find("QuestIMG").GetComponent<Image>().sprite != null)
                PlayerPrefs.SetString("QuestIMG", GameObject.Find("QuestIMG").GetComponent<Image>().sprite.name);
            PlayerPrefsX.SetColor("QuestColor", GameObject.Find("QuestIMG").GetComponent<Image>().color);
            if (GameObject.FindGameObjectWithTag("NextLevel") != null)
                PlayerPrefsX.SetBool("NextLevel", GameObject.FindGameObjectWithTag("NextLevel").GetComponent<BoxCollider>().enabled);
            if (GameObject.FindGameObjectsWithTag("Barreira") != null)
            {
                for (int x = 0; x < GameObject.FindGameObjectsWithTag("Barreira").Length; x++)
                {
                    PlayerPrefsX.SetBool("BarreirasObs", GameObject.FindGameObjectsWithTag("Barreira")[x].GetComponent<NavMeshObstacle>().enabled);
                    PlayerPrefsX.SetBool("BarreirasCol", GameObject.FindGameObjectsWithTag("Barreira")[x].GetComponent<BoxCollider>().enabled);
                    PlayerPrefsX.SetBool("BarreirasRend", GameObject.FindGameObjectsWithTag("Barreira")[x].GetComponent<MeshRenderer>().enabled);
                }
            }
            savingTxt.GetComponent<Text>().text = "Saving...";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        savingTxt.GetComponent<Text>().text = "Saving...";
    }

    private void OnTriggerExit(Collider other)
    {
        savingTxt.GetComponent<Text>().text = "";
        PlayerPrefs.Save();
    }
}
