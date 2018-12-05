using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using System.Threading;

public enum ESTADO_INIMIGOCHAVE { OCIOSO, ATACANDO, DEFENDENDO, DERROTADO }
public enum DIRECAO_INIMIGOCHAVE { INDO, VOLTANDO, PARADO }
public class InimigoChave : MonoBehaviour
{
    public string eTypeName;
    Camera cam;
    GameObject enemyShield;
    Material shieldMaterial;
    float shieldTimer;
    public List<Vector3> wayPositions;

    public int waypoint;
    bool going;

    bool spawned;
    DIRECAO_INIMIGOCHAVE direcao;
    public NavMeshAgent enemyAgent;

    public string enemyRouteInfo;

    public float fieldOfView;
    Vector3 playerDirection;
    [SerializeField]
    float playerAngle;
    [SerializeField]
    GameObject player;
    public bool ableToWalk;
    bool walking;
    public ESTADO_INIMIGOCHAVE estado;
    public bool playerOnSight;
    float sightRange;
    System.Random rnd;
    public float stunTimer;
    public float alertTimer;
    [SerializeField]
    public GameObject shootSeeker;
    float stopTime;
    GameObject[] enemies;
    bool beenShot;
    Quaternion shotRotation;
    float peaceTimer;
    bool peace;
    public Animator enemyAnim;
    Vector3 shotDirection;
    public float enemySpeed = 5;
    float lessSpeed = 1;
    AudioSource eAudio;
    void Start()
    {
        enemyShield = GameObject.FindGameObjectWithTag("EnemyShield");
        shieldMaterial = enemyShield.GetComponent<Renderer>().material;
        enemySpeed = 5;
        enemyAnim = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();
        stopTime = 0;
        sightRange = 25;
        enemyRouteInfo = this.gameObject.name.Remove(0, 7);
        if (wayPositions.Count > 0)
        {
            for (int x = 0; x < wayPositions.Count; x++)
            {
                enemyRouteInfo += "/" + wayPositions[x];
            }
        }
        else
            enemyRouteInfo += this.gameObject.transform.position;
        spawned = true;
        waypoint = 0;
        ableToWalk = true;
        cam = Camera.main;
        eAudio = GetComponent<AudioSource>();
        GameObject.Find("Barreira1").GetComponent<NavMeshObstacle>().enabled = false;
        GameObject.Find("Barreira1").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Barreira1").GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Barreira2").GetComponent<NavMeshObstacle>().enabled = false;
        GameObject.Find("Barreira2").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Barreira2").GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (this != null)
        {
            player = GameObject.Find("June(Clone)");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (peace)
            {
                peaceTimer += Time.deltaTime;
                ableToWalk = false;
                if (peaceTimer >= 5.0f)
                {
                    peaceTimer = 0;
                    ableToWalk = true;
                    peace = false;
                }
            }
            else
            {
                ableToWalk = true;
            }
            //Se o inimigo não tiver sido stunnado e não estiver sob efeito pacificador
            if (ableToWalk)
            {
                if (estado != ESTADO_INIMIGOCHAVE.DERROTADO)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) <= 20)
                    {
                        GameObject.Find("Barreira1").GetComponent<NavMeshObstacle>().enabled = true;
                        GameObject.Find("Barreira1").GetComponent<MeshRenderer>().enabled = true;
                        GameObject.Find("Barreira1").GetComponent<BoxCollider>().enabled = true;
                        GameObject.Find("Barreira2").GetComponent<NavMeshObstacle>().enabled = true;
                        GameObject.Find("Barreira2").GetComponent<MeshRenderer>().enabled = true;
                        GameObject.Find("Barreira2").GetComponent<BoxCollider>().enabled = true;
                    }
                }
                enemyAnim.SetBool("Stun", false);
                playerDirection = (player.transform.position + 2.5f * player.transform.up) - (transform.position + 2.5f * transform.up);
                playerAngle = Vector3.Angle(playerDirection, transform.forward);
                //Verificar se o player está no campo de visão
                if (playerAngle <= fieldOfView / 2)
                {
                    RaycastHit hit;
                    //Verificar se não há obstáculos visuais entre o inimigo e o player
                    if (Physics.Raycast(transform.position + 2.5f * transform.up, playerDirection.normalized, out hit, sightRange, ~(1 >> 10)))
                    {
                        Debug.DrawRay(transform.position + 2.5f * transform.up, playerDirection, Color.red);
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            playerOnSight = true;
                        }
                        else
                            playerOnSight = false;
                    }
                }
                if (playerOnSight)
                {
                    for (int x = 0; x < enemies.Length; x++)
                    {
                        if (Vector3.Distance(transform.position, enemies[x].transform.position) <= 25)
                        {
                            if (enemies[x].GetComponent<MoverInimigo>() != null)
                                enemies[x].GetComponent<MoverInimigo>().estado = ESTADO_INIMIGO.PERSEGUINDO;
                        }
                    }
                }
                if (beenShot)
                {
                    if ((Mathf.Approximately(transform.rotation.w, shotRotation.w) && Mathf.Approximately(transform.rotation.y, shotRotation.y)) ||
                        estado == ESTADO_INIMIGOCHAVE.ATACANDO)
                    {
                        beenShot = false;
                    }
                    else
                    {
                        float compareAngle = Vector3.SignedAngle(transform.forward, shotDirection, Vector3.up);
                        enemyAnim.SetFloat("Turn", compareAngle);
                        transform.rotation = Quaternion.Lerp(transform.rotation, shotRotation, 2f * Time.deltaTime);
                    }
                }
                else
                {
                    enemyAnim.SetFloat("Turn", 0);
                }

                switch (estado)
                {
                    case ESTADO_INIMIGOCHAVE.OCIOSO:
                        GetComponentInChildren<Dialogue>().enabled = false;
                        enemyShield.GetComponent<Collider>().enabled = false;
                        fieldOfView = 150f;
                        sightRange = 25;
                        enemyAgent.speed = enemySpeed;
                        if (enemyAgent.remainingDistance <= 0)
                        {
                            enemyAnim.SetFloat("Velocity", 0);
                            enemyAgent.isStopped = true;
                        }
                        else
                        {
                            enemyAnim.SetFloat("Velocity", enemyAgent.speed);
                            enemyAgent.isStopped = false;
                        }

                        if (wayPositions.Count > 1)
                        {
                            if ((enemyAgent.transform.position.x == wayPositions[0].x) && (enemyAgent.transform.position.z == wayPositions[0].z))
                            {
                                direcao = DIRECAO_INIMIGOCHAVE.INDO;
                            }
                            else if ((enemyAgent.transform.position.x == wayPositions[wayPositions.Count - 1].x) && (enemyAgent.transform.position.z == wayPositions[wayPositions.Count - 1].z))
                            {
                                direcao = DIRECAO_INIMIGOCHAVE.VOLTANDO;
                            }
                            Direction(direcao);
                            walking = true;
                        }
                        else if (wayPositions.Count == 1)
                        {
                            direcao = DIRECAO_INIMIGOCHAVE.PARADO;
                        }

                        if (playerOnSight && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().isDead)
                        {
                            eAudio.PlayOneShot(GetComponent<Audio>().clips[8]);
                            estado = ESTADO_INIMIGOCHAVE.ATACANDO;
                        }

                        if (GetComponent<KeyEnemyHealth>().isDead)
                        {
                            estado = ESTADO_INIMIGOCHAVE.DERROTADO;
                        }
                        break;

                    case ESTADO_INIMIGOCHAVE.ATACANDO:
                        GetComponentInChildren<Dialogue>().enabled = false;
                        GetComponent<AtaqueInimigoChave>().enabled = true;
                        enemyAnim.SetBool("Defend", false);
                        if (shieldMaterial.GetFloat("_StartPoint") < 0.5f)
                            enemyShield.GetComponent<Collider>().enabled = false;
                        fieldOfView = 360;
                        sightRange = 50;
                        enemyAgent.speed = 2 * enemySpeed;
                        enemyAgent.SetDestination(player.transform.position);
                        if (enemyAgent.remainingDistance <= 4)
                        {
                            if (player.GetComponent<ControleTeclado>().direcaoTeclado.magnitude > 0 || player.GetComponent<ControleMouse>().direction.magnitude > 0)
                            {
                                if (Input.GetKey(KeyCode.LeftShift))
                                {
                                    enemyAnim.SetFloat("Velocity", enemyAgent.speed);
                                }
                                else
                                    enemyAnim.SetFloat("Velocity", 5);
                                enemyAgent.isStopped = false;
                            }
                            else
                            {
                                enemyAnim.SetFloat("Velocity", 0);
                                enemyAgent.isStopped = true;
                            }
                        }
                        if (GetComponent<KeyEnemyHealth>().isDead)
                        {
                            estado = ESTADO_INIMIGOCHAVE.DERROTADO;
                        }
                        else
                        {
                            enemyAgent.isStopped = false;
                            enemyAnim.SetFloat("Velocity", enemyAgent.speed);
                        }
                        if (shieldTimer > 0)
                        {
                            shieldTimer -= Time.deltaTime;
                        }
                        if (shieldMaterial.GetFloat("_StartPoint") >= -5f)
                        {
                            shieldMaterial.SetFloat("_StartPoint", shieldTimer);
                        }
                        if (GetComponent<KeyEnemyHealth>().timesAttacked >= 10)
                        {
                            shieldTimer = 0;
                            estado = ESTADO_INIMIGOCHAVE.DEFENDENDO;
                        }
                        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().isDead)
                        {
                            estado = ESTADO_INIMIGOCHAVE.OCIOSO;
                        }
                        break;
                    case ESTADO_INIMIGOCHAVE.DEFENDENDO:
                        GetComponentInChildren<Dialogue>().enabled = false;
                        enemyAnim.SetFloat("Velocity", 0);
                        enemyAnim.SetBool("Defend", true);
                        GetComponent<AtaqueInimigoChave>().enabled = false;
                        enemyAgent.isStopped = true;
                        enemyShield.GetComponent<Collider>().enabled = true;
                        shieldTimer += Time.deltaTime;
                        if (GetComponent<KeyEnemyHealth>().isDead)
                        {
                            estado = ESTADO_INIMIGOCHAVE.DERROTADO;
                        }
                        if (shieldMaterial.GetFloat("_StartPoint") < 1.5f)
                        {
                            shieldMaterial.SetFloat("_StartPoint", shieldTimer);
                        }
                        else
                        {
                            shieldMaterial.SetFloat("_StartPoint", 1.5f);
                        }
                        if (GetComponent<KeyEnemyHealth>().enemyCurrentHealth > -1)
                        {
                            if (shieldTimer >= 15)
                            {
                                shieldTimer = 1.5f;
                                GetComponent<KeyEnemyHealth>().timesAttacked = 0;
                                estado = ESTADO_INIMIGOCHAVE.ATACANDO;
                            }
                        }
                        break;
                    case ESTADO_INIMIGOCHAVE.DERROTADO:
                        enemyAnim.SetLayerWeight(1, 0);
                        enemyAnim.SetLayerWeight(2, 0);
                        enemyAnim.SetFloat("Velocity", 0);
                        enemyAnim.SetBool("Defend", false);
                        enemyAnim.SetBool("Defeated", true);
                        GetComponent<AtaqueInimigoChave>().enabled = false;
                        enemyAgent.isStopped = true;
                        if (shieldTimer > 0)
                        {
                            shieldTimer -= Time.deltaTime;
                        }
                        if (shieldMaterial.GetFloat("_StartPoint") >= -5f)
                        {
                            shieldMaterial.SetFloat("_StartPoint", shieldTimer);
                        }
                        if (shieldMaterial.GetFloat("_StartPoint") < 0.5f)
                            enemyShield.GetComponent<Collider>().enabled = false;
                        GetComponentInChildren<Dialogue>().enabled = true;
                        break;


                }

            }
            else
            {
                estado = ESTADO_INIMIGOCHAVE.ATACANDO;
                enemyShield.GetComponent<Collider>().enabled = false;
                if (shieldMaterial.GetFloat("_StartPoint") > -3f)
                {
                    shieldTimer -= Time.deltaTime;
                    shieldMaterial.SetFloat("_StartPoint", shieldTimer);
                }
                enemyAgent.isStopped = true;
                stunTimer += Time.deltaTime;
                enemyAnim.SetBool("Stun", true);
                if (stunTimer >= 5f)
                {
                    GetComponent<KeyEnemyHealth>().timesAttacked = 0;
                    ableToWalk = true;
                    stunTimer = 0f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemySpawnPoint"))
        {

            if (wayPositions.Count > 0 && spawned)
            {
                waypoint = 0;
                enemyAgent.SetDestination(wayPositions[0]);
                spawned = false;
            }
            Destroy(other.gameObject);
        }
    }

    void Direction(DIRECAO_INIMIGOCHAVE direction)
    {
        switch (direction)
        {
            case DIRECAO_INIMIGOCHAVE.INDO:
                if ((enemyAgent.transform.position.x == wayPositions[waypoint].x) && (enemyAgent.transform.position.z == wayPositions[waypoint].z))
                {
                    stopTime += Time.deltaTime;

                    if (stopTime >= 8f)
                    {

                        enemyAgent.SetDestination(wayPositions[waypoint + 1]);
                        waypoint++;
                        stopTime = 0;
                    }
                }
                break;
            case DIRECAO_INIMIGOCHAVE.VOLTANDO:
                if ((enemyAgent.transform.position.x == wayPositions[waypoint].x) && (enemyAgent.transform.position.z == wayPositions[waypoint].z))
                {
                    stopTime += Time.deltaTime;

                    if (stopTime >= 8f)
                    {

                        enemyAgent.SetDestination(wayPositions[waypoint - 1]);
                        waypoint--;
                        stopTime = 0;
                    }
                }
                break;
            case DIRECAO_INIMIGOCHAVE.PARADO:
                enemyAgent.SetDestination(new Vector3(wayPositions[0].x, enemyAgent.transform.position.y, wayPositions[0].z));
                break;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shoot"))
        {
            if (estado == ESTADO_INIMIGOCHAVE.OCIOSO)
                estado = ESTADO_INIMIGOCHAVE.ATACANDO;
            alertTimer = 0.0f;
            shootSeeker.transform.LookAt(other.transform.position);
            shotDirection = other.transform.position - transform.position;
            shotRotation = shootSeeker.transform.rotation;
            beenShot = true;
            if (!ableToWalk)
                GetComponent<KeyEnemyHealth>().timesAttacked = 0;
        }
        if (other.CompareTag("Peace"))
        {
            if (estado == ESTADO_INIMIGOCHAVE.DEFENDENDO)
                shieldTimer = 1.5f;
            peace = true;
            GetComponent<KeyEnemyHealth>().timesAttacked = 0;
        }
    }

}

