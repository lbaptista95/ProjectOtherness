using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControleMouse : MonoBehaviour
{
    Camera cam;
    public float timeBetweenPlayerAttacks = 0.5f;
    public int playerDamage = 10;
    float timer;
    public bool pursueEnemy;
    public bool enemyInRange;
    public ParticleSystem shot;

    public ControleTeclado cTeclado;
    EnemyHealth enemyHealth;
    [SerializeField]
    GameObject enemy;
    public NavMeshAgent playerAgent;
    RaycastHit clickPos;
    RaycastHit aim;
    Vector3 aimPoint;
    [SerializeField]
    GameObject mainProjectile;
    public GameObject staff;
    public float stunTime;
    public float noise;

    public Animator juneAnim;
    public bool isMoving;
    public GameObject aimObject;
    Rigidbody playerRigidbody;
    Vector3 mousePos;
    Vector3 movement;
    public float speed;
    int floorMask;
    float camRayLength = 1000f;
    float h;
    float v;
    public Animation turnRight;
    public Animation turnLeft;
    public GameObject globalOrientation;
    public GameObject torso;
    float heading = 0;
    Vector2 input;
    public GameObject cursor;
    Vector3 playerToMouse;
    Quaternion newRotation;
    float compareAngle;
    Vector3 camR;
    Vector3 camF;
    AudioSource audioSource;
    public Vector3 direction;
    GameObject juneCol;
    float shotTimer;
    void Start()
    {
        shot = GameObject.FindGameObjectWithTag("Shoot").GetComponent<ParticleSystem>();
        torso = GameObject.Find("Col_B");
        globalOrientation = GameObject.Find("GlobalOrientation(Clone)");
        floorMask = LayerMask.GetMask("ground");
        playerRigidbody = GetComponent<Rigidbody>();
        cTeclado = GetComponent<ControleTeclado>();
        juneAnim = GetComponent<Animator>();
        cam = Camera.main;
        staff = GameObject.FindGameObjectWithTag("Staff");
        playerAgent = GetComponent<NavMeshAgent>();
        aimObject = GameObject.Find("Aim");
        cursor = GameObject.Find("Cursor");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Turning();
        Animating(h, v);
        if (Input.GetMouseButton(1))
        {
            juneAnim.SetBool("Gun", true);
            juneAnim.SetLayerWeight(1, 1);

        }
        else
        {
            juneAnim.SetLayerWeight(1, 0);
            juneAnim.SetBool("Gun", false);
        }
    }
    void LateUpdate()
    {
        if (this.enabled)
        {
            juneCol = GameObject.FindGameObjectWithTag("JuneColuna");
            shot = GameObject.FindGameObjectWithTag("Shoot").GetComponent<ParticleSystem>();
            juneAnim = GetComponent<Animator>();
            playerAgent.speed = 5;
            playerAgent.angularSpeed = 0;
            playerAgent.acceleration = 1000;
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            globalOrientation = GameObject.Find("GlobalOrientation(Clone)");
            speed = 5f;
            Move(h, v);
            Vector3 cursorDirection = cursor.transform.position - transform.position;
            cursorDirection.y = 0;
            Quaternion cursorRotation = Quaternion.LookRotation(cursorDirection);
            juneCol.transform.rotation = cursorRotation;
            shotTimer += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if (shot != null)
                {
                    if (shotTimer >= 0.35f)
                    {
                        audioSource.PlayOneShot(GetComponent<Audio>().clips[4]);
                        shot.Emit(1);
                        shotTimer = 0;
                    }
                }
            }
            Vector3 teclado = new Vector3(h, 0, v);
            if (teclado.magnitude>0)
            {
                juneAnim.SetLayerWeight(2, 0.5f);
            }
            else
            {
                juneAnim.SetLayerWeight(2, 1f);
            }           

        }

    }

    void Move(float h, float v)
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        input = new Vector2(h, v);
        Vector2.ClampMagnitude(input, 1);
        camF = cam.transform.forward;
        camR = cam.transform.right;
        camF.y = camR.y = 0;
        camF.Normalize();
        camR.Normalize();
        direction = new Vector3(h, 0, v);
        direction.Normalize();
        Vector3 destiny = cam.gameObject.transform.TransformDirection(direction);
        if (direction.magnitude > 0)
            playerAgent.isStopped = false;
        else
            playerAgent.isStopped = true;
        playerAgent.SetDestination(transform.position + destiny);
        Debug.DrawLine(transform.position, destiny, Color.blue);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = cursor.transform.position - transform.position;
            playerToMouse.y = 0f;
            newRotation = Quaternion.LookRotation(playerToMouse);
            compareAngle = Vector3.SignedAngle(transform.forward, playerToMouse, Vector3.up);            
            juneAnim.SetFloat("Turn", compareAngle);            
        }
    }

    void Animating(float h, float v)
    {
        if (Vector3.Angle(transform.forward, camF) < 30)
        {
            juneAnim.SetFloat("Velocity", v);
            juneAnim.SetFloat("Side", h);
        }
        else if (Vector3.Angle(transform.forward, -camF) < 30)
        {
            juneAnim.SetFloat("Velocity", -v);
            juneAnim.SetFloat("Side", -h);
        }
        else if (Vector3.Angle(transform.forward, camR) < 30)
        {
            juneAnim.SetFloat("Velocity", h);
            juneAnim.SetFloat("Side", -v);
        }
        else if (Vector3.Angle(transform.forward, -camR) < 30)
        {
            juneAnim.SetFloat("Velocity", -h);
            juneAnim.SetFloat("Side", v);
        }
        else if (Vector3.Angle(transform.forward, globalOrientation.transform.forward) <= 30)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                juneAnim.SetFloat("Velocity", v);
                juneAnim.SetFloat("Side", v);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                juneAnim.SetFloat("Velocity", -h);
                juneAnim.SetFloat("Side", h);
            }
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {
                juneAnim.SetFloat("Velocity", v);
                juneAnim.SetFloat("Side", 0);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
            {
                juneAnim.SetFloat("Velocity", 0);
                juneAnim.SetFloat("Side", h);
            }
        }
        else if (Vector3.Angle(transform.forward, -globalOrientation.transform.forward) <= 30)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                juneAnim.SetFloat("Velocity", -v);
                juneAnim.SetFloat("Side", -v);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                juneAnim.SetFloat("Velocity", h);
                juneAnim.SetFloat("Side", -h);
            }
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {
                juneAnim.SetFloat("Velocity", -v);
                juneAnim.SetFloat("Side", 0);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
            {
                juneAnim.SetFloat("Velocity", 0);
                juneAnim.SetFloat("Side", -h);
            }
        }
        else if (Vector3.Angle(transform.forward, -globalOrientation.transform.right) <= 30)
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                juneAnim.SetFloat("Velocity", -v);
                juneAnim.SetFloat("Side", v);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                juneAnim.SetFloat("Velocity", -h);
                juneAnim.SetFloat("Side", -h);
            }
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {
                juneAnim.SetFloat("Velocity", 0);
                juneAnim.SetFloat("Side", v);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
            {
                juneAnim.SetFloat("Velocity", -v);
                juneAnim.SetFloat("Side", 0);
            }
        }
        else if (Vector3.Angle(transform.forward, globalOrientation.transform.right) <= 30)
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                juneAnim.SetFloat("Velocity", v);
                juneAnim.SetFloat("Side", -v);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                juneAnim.SetFloat("Velocity", h);
                juneAnim.SetFloat("Side", h);
            }
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {
                juneAnim.SetFloat("Velocity", 0);
                juneAnim.SetFloat("Side", -v);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
            {
                juneAnim.SetFloat("Velocity", v);
                juneAnim.SetFloat("Side", 0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
        }
    }
    
}
