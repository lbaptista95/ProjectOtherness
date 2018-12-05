using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("O manager nao esta na cena");
            }
            return instance;
        }
    }

    public string[] enemiesTypesNames;

    public string hintText;
    public Text hint;
    public Text gameOver;
    public Button gReturnMenu;
    public Button gReturnCheck;
    public Image gImage;

    public float deaths;
    public GameObject saveManager;
    public GameObject junePrefab;
    public GameObject enemyPrefab;
    public GameObject juneSpawn;
    public GameObject[] enemySpawn;

    int rW;
    int rH;
    int qua;

    public string[] enemyCode;
    public string[] splitEnemyCode;
    public int enemyNumber;
    public Vector3[] enemyPoints;
    public GameObject globalOrientation;

    public GameObject juneInstance;
    public GameObject enemyInstance;
    public int enemySpawnCount;

    public CamCTRL myCamera;

    public Quaternion[] eRotation;
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool isDead;
    bool damaged;
    public float deathTimer = 0;
    float deadTimer;
    //Animator anim;

    ControleMouse controleMouse;



    private void Awake()
    {
        deadTimer = 0;
        hint.enabled = false;
        gameOver.enabled = false;
        gReturnMenu.enabled = false;
        gReturnCheck.enabled = false;
        gReturnMenu.GetComponentInChildren<Text>().enabled = false;
        gReturnCheck.GetComponentInChildren<Text>().enabled = false;
        gImage.enabled = false;

        deaths = 0;


        if (instance != null)
        {
            Debug.LogError("Há mais de um manager na cena");
        }
        instance = this;
        juneSpawn = GameObject.FindGameObjectWithTag("JuneSpawnPoint");
        enemySpawn = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        saveManager = GameObject.FindGameObjectWithTag("SaveManager");
        if (saveManager != null)
        {
            if (saveManager.GetComponent<SaveManager>().gameLoaded)
            {
                LoadCheckpoint();
            }
            else
            {
                StartGame();
                currentHealth = startingHealth;
            }
        }
        else
        {
            StartGame();
        }

        controleMouse = juneInstance.GetComponent<ControleMouse>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.Find("June(Clone)") != null)
            if (GameObject.Find("June(Clone)").GetComponent<ControleTeclado>().isGiraffe)
            {
                if (deaths >= 5)
                {
                    deaths = 0;
                    GameObject.Find("ViolenceIMG").GetComponent<Image>().enabled = true;
                    if (GameObject.Find("Audio Source") != null)
                        GameObject.Find("Audio Source").GetComponent<AudioSource>().enabled = false;
                    juneInstance.GetComponent<AlternarControles>().enabled = false;
                    juneInstance.GetComponent<ControleTeclado>().enabled = false;
                    juneInstance.GetComponent<ControleMouse>().enabled = false;
                    hintText = "Violence  comes  from  the  belief  that  other  people  make  us  suffer  and  therefore  deserve  to  be  punished";
                    GameOver();

                }
                if (deaths > 0)
                {
                    deathTimer += Time.deltaTime;
                }
                if (deathTimer >= 30)
                {
                    deaths--;
                    deathTimer = 0;
                }
            }
        if (isDead && deadTimer <= 3)
        {
            deadTimer += Time.deltaTime;
        }
        if (deadTimer >= 3)
        {
            GameOver();
            deadTimer = 0;
        }
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        juneInstance.GetComponentInChildren<MeleeAttack>().enabled = false;
        juneInstance.GetComponent<NavMeshAgent>().enabled = false;
        juneInstance.GetComponent<AlternarControles>().controleComum.enabled = false;
        juneInstance.GetComponent<AlternarControles>().enabled = false;
        juneInstance.GetComponent<ControleMouse>().enabled = false;
        juneInstance.GetComponent<ControleTeclado>().enabled = false;
        juneInstance.GetComponent<Animator>().enabled = false;
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public void LoadCheckpoint()
    {
        if (GameObject.Find("Audio Source") != null)
            GameObject.Find("Audio Source").GetComponent<AudioSource>().enabled = true;
        Time.timeScale = 1;
        hint.enabled = false;
        gameOver.enabled = false;
        gReturnMenu.enabled = false;
        gReturnCheck.enabled = false;
        gReturnMenu.GetComponentInChildren<Text>().enabled = false;
        gReturnCheck.GetComponentInChildren<Text>().enabled = false;
        gImage.enabled = false;
        if (PlayerPrefs.HasKey("Volume"))
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        //SceneManager.LoadScene(PlayerPrefs.GetString("Cena"));
        Instantiate(globalOrientation);
        juneInstance = Instantiate(junePrefab, PlayerPrefsX.GetVector3("Personagem"), Quaternion.identity);
        juneInstance.GetComponent<ControleTeclado>().isGiraffe = PlayerPrefsX.GetBool("Girafa");
        eRotation = PlayerPrefsX.GetQuaternionArray("RotacaoInimigos");
        currentHealth = PlayerPrefs.GetInt("PlayerHealth");
        enemiesTypesNames = PlayerPrefsX.GetStringArray("TiposDeInimigo");
        GameObject.Find("Quest").GetComponent<Text>().text = PlayerPrefs.GetString("Mission");
        juneInstance.GetComponent<AlternarControles>().mission = PlayerPrefsX.GetBool("MissionBool");
        GameObject.Find("QuestIMG").GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("QuestIMG"));
        GameObject.Find("QuestIMG").GetComponent<Image>().color = PlayerPrefsX.GetColor("QuestColor");
        GameObject.FindGameObjectWithTag("NextLevel").GetComponent<BoxCollider>().enabled = PlayerPrefsX.GetBool("NextLevel");
        if (GameObject.FindGameObjectsWithTag("Barreira") != null)
        {
            for (int x = 0; x < GameObject.FindGameObjectsWithTag("Barreira").Length; x++)
            {
                GameObject.FindGameObjectsWithTag("Barreira")[x].GetComponent<NavMeshObstacle>().enabled = PlayerPrefsX.GetBool("BarreirasObs");
                GameObject.FindGameObjectsWithTag("Barreira")[x].GetComponent<BoxCollider>().enabled = PlayerPrefsX.GetBool("BarreirasCol");
                GameObject.FindGameObjectsWithTag("Barreira")[x].GetComponent<MeshRenderer>().enabled = PlayerPrefsX.GetBool("BarreirasRend");
            }
        }
        if (PlayerPrefs.HasKey("Inimigos"))
        {
            enemyCode = PlayerPrefsX.GetStringArray("Inimigos");
            for (int x = 0; x < enemyCode.Length; x++)
            {
                splitEnemyCode = enemyCode[x].Split('/');
                enemyNumber = int.Parse(splitEnemyCode[0]);
                enemyPoints = new Vector3[splitEnemyCode.Length - 1];
                for (int w = 1; w < splitEnemyCode.Length; w++)
                {
                    enemyPoints[w - 1] = StringToVector3(splitEnemyCode[w]);
                }
                print("ROTACAO CARREGADA: " + eRotation[x]);
                enemyPrefab = Resources.Load("EnemyPrefab/" + enemiesTypesNames[x]) as GameObject;
                enemyInstance = Instantiate(enemyPrefab, enemyPoints[0], eRotation[x]);
                enemyInstance.name = "Inimigo" + enemyNumber;
                if (enemyInstance.CompareTag("Enemy"))
                {
                    if (enemyInstance.GetComponent<MoverInimigo>() != null)
                    {
                        enemyInstance.GetComponent<MoverInimigo>().waypoint = 0;
                        enemyInstance.GetComponent<MoverInimigo>().eTypeName = enemiesTypesNames[x];
                    }
                    else
                    {
                        enemyInstance.GetComponent<InimigoChave>().waypoint = 0;
                        enemyInstance.GetComponent<InimigoChave>().eTypeName = enemiesTypesNames[x];
                    }
                }
                enemyInstance.GetComponentInChildren<NonRotateHealthBar>().rotation = PlayerPrefsX.GetQuaternionArray("Canvas")[x];
                for (int z = 0; z < enemyPoints.Length; z++)
                {
                    if (enemyInstance.CompareTag("Enemy"))
                    {
                        if (enemyInstance.GetComponent<MoverInimigo>() != null)
                            enemyInstance.GetComponent<MoverInimigo>().wayPositions.Add(enemyPoints[z]);
                        else
                            enemyInstance.GetComponent<InimigoChave>().wayPositions.Add(enemyPoints[z]);
                    }
                }
                if (enemyInstance.GetComponent<InimigoChave>() != null)
                {
                    enemyInstance.GetComponent<KeyEnemyHealth>().enemyCurrentHealth = PlayerPrefs.GetFloat("EnemyLife");
                    if (enemyInstance.GetComponent<KeyEnemyHealth>().enemyCurrentHealth <= 0)
                    {
                        enemyInstance.GetComponent<Dialogue>().enabled = false;
                    }
                }
            }
        }
        for (int y = 0; y < enemySpawn.Length; y++)
        {
            Destroy(enemySpawn[y]);
        }
        GameObject.Find("ViolenceIMG").GetComponent<Image>().enabled = false;
    }

    public void StartGame()
    {
        if (GameObject.Find("Audio Source") != null)
            GameObject.Find("Audio Source").GetComponent<AudioSource>().enabled = true;
        Time.timeScale = 1;
        hint.enabled = false;
        gameOver.enabled = false;
        gReturnMenu.enabled = false;
        gReturnCheck.enabled = false;
        gImage.enabled = false;
        gReturnMenu.GetComponentInChildren<Text>().enabled = false;
        gReturnCheck.GetComponentInChildren<Text>().enabled = false;
        if (PlayerPrefs.HasKey("Volume"))
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        Instantiate(globalOrientation);
        if (PlayerPrefs.HasKey("ResolutionW"))
        {
            print("SETANDO CONFIGURAÇÕES DE VIDEO");
            int rW = PlayerPrefs.GetInt("ResolutionW");
            int rH = PlayerPrefs.GetInt("ResolutionH");
            int qua = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(qua);
            Screen.SetResolution(rW, rH, Screen.fullScreen);
        }

        juneInstance = Instantiate(junePrefab, juneSpawn.transform.position, Quaternion.identity);
        if (saveManager != null)
        {
            juneInstance.GetComponent<ControleTeclado>().isGiraffe = saveManager.GetComponent<SaveManager>().isJuneGiraffe;
            GameObject.Find("Quest").GetComponent<Text>().text = saveManager.GetComponent<SaveManager>().mission;
            juneInstance.GetComponentInParent<AlternarControles>().mission = saveManager.GetComponent<SaveManager>().missionBool;
            GameObject.Find("QuestIMG").GetComponent<Image>().color = saveManager.GetComponent<SaveManager>().questColor;
            GameObject.Find("QuestIMG").GetComponent<Image>().sprite = Resources.Load<Sprite>(saveManager.GetComponent<SaveManager>().questSprite);
        }
        for (int x = 0; x < enemySpawn.Length; x++)
        {
            enemyPrefab = Resources.Load("EnemyPrefab/" + enemySpawn[x].GetComponent<CreateEnemyRoute>().enemyType.name) as GameObject;
            enemyInstance = Instantiate(enemyPrefab, enemySpawn[x].transform.position, Quaternion.identity);
            if (enemyInstance.CompareTag("Enemy"))
            {
                if (enemyInstance.GetComponent<MoverInimigo>() != null)
                    enemyInstance.GetComponent<MoverInimigo>().eTypeName = enemySpawn[x].GetComponent<CreateEnemyRoute>().enemyType.name;
                else
                    enemyInstance.GetComponent<InimigoChave>().eTypeName = enemySpawn[x].GetComponent<CreateEnemyRoute>().enemyType.name;
            }
            for (int y = 0; y < enemySpawn[x].GetComponent<CreateEnemyRoute>().waypoints.Length; y++)
            {
                if (enemyInstance.CompareTag("Enemy"))
                {
                    if (enemyInstance.GetComponent<MoverInimigo>() != null)
                        enemyInstance.GetComponent<MoverInimigo>().wayPositions.Add(enemySpawn[x].GetComponent<CreateEnemyRoute>().waypoints[y].transform.position);
                    else
                        enemyInstance.GetComponent<InimigoChave>().wayPositions.Add(enemySpawn[x].GetComponent<CreateEnemyRoute>().waypoints[y].transform.position);
                }
                Destroy(enemySpawn[x].GetComponent<CreateEnemyRoute>().waypoints[y]);
            }
            enemyInstance.name = "Inimigo" + enemySpawnCount;
            enemySpawnCount++;
        }
        currentHealth = startingHealth;
        GameObject.Find("ViolenceIMG").GetComponent<Image>().enabled = false;
    }

    public void GameOver()
    {
        Cursor.visible = true;
        hint.enabled = true;
        gameOver.enabled = true;
        gReturnMenu.enabled = true;
        gReturnCheck.enabled = true;
        gImage.enabled = true;
        gReturnMenu.GetComponentInChildren<Text>().enabled = true;
        gReturnCheck.GetComponentInChildren<Text>().enabled = true;
        Hint();
    }

    public void Hint()
    {
        if (hintText != null)
            hint.text = hintText;
        else
            hint.text = "";
    }

    public void ReturnToLastCheckpoint()
    {
        saveManager.GetComponent<SaveManager>().gameLoaded = true;
        saveManager.GetComponent<SaveManager>().nextScene = PlayerPrefs.GetString("Cena");
        SceneManager.LoadScene("Loading");
    }

}
