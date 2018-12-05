using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class ControleTeclado : MonoBehaviour
{

    public NavMeshAgent pAgent;
    public Vector3 direcaoTeclado;
    Camera mycamera;
    public float speed;
    Animator anim;
    Rigidbody rgb;
    public bool staffOut;
    public Material staffMaterial;
    public float staffTimer;
    public bool hideGun;
    public GameObject staff;
    public float staffRange;
    public GameObject ability1;
    public ParticleSystem[] ability1Particles;
    public GameObject abilityAttractor;
    GameObject cursor;
    public float noise;
    float peaceTimer;
    AudioSource audioSource;
    public bool isGiraffe;
    GameObject stun;
    // Use this for initialization
    void Start()
    {
        peaceTimer = 0;
        cursor = GameObject.Find("Cursor");
        ability1 = GameObject.Find("Habilidade_1");
        ability1Particles = ability1.GetComponentsInChildren<ParticleSystem>();
        abilityAttractor = GameObject.Find("BolaCacete");
        staff = GameObject.FindGameObjectWithTag("Staff");
        staffMaterial = staff.GetComponent<Renderer>().material;
        mycamera = Camera.main;
        pAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody>();
        staffMaterial.SetFloat("_DissolveAmount", 1);
        staffRange = 15f;
        staffTimer = 1;
        audioSource = GetComponent<AudioSource>();
        stun = GameObject.Find("Stun");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled && pAgent != null)
        {
            pAgent = GetComponent<NavMeshAgent>();
            pAgent.enabled = true;
            pAgent.angularSpeed = 1000;
            pAgent.isStopped = false;
            staffMaterial.SetFloat("_DissolveAmount", staffTimer);
            direcaoTeclado = VetorTeclado();
            if (direcaoTeclado.magnitude > 0)
            {
                anim.SetBool("Walk", true);
                Vector3 destiny = mycamera.gameObject.transform.TransformDirection(direcaoTeclado);
                destiny.Normalize();
                destiny.y = 0;
                pAgent.SetDestination(transform.position + destiny);
            }
            else
            {
                pAgent.isStopped = true;
                anim.SetBool("Walk", false);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (direcaoTeclado.magnitude > 0)
                {
                    pAgent.acceleration = 15;
                    pAgent.speed = 12f;
                    anim.SetBool("Run", true);
                    noise = 5.0f;
                }
                else
                {
                    pAgent.acceleration = 10;
                    pAgent.speed = 5f;
                    anim.SetBool("Run", false);
                    noise = 0.5f;
                }
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                pAgent.speed = 2f;
                anim.SetBool("Crouch", true);
                noise = 0.5f;
            }
            else
            {
                if (anim.GetBool("Walk"))
                    noise = 3.0f;
                else
                {
                    noise = 0.5f;
                }
                pAgent.acceleration = 10;
                pAgent.speed = 5f;
                anim.SetBool("Crouch", false);
                anim.SetBool("Run", false);
            }
            if (isGiraffe)
            {
                for (int x=0;x< GameObject.FindGameObjectWithTag("JuneMask").GetComponentsInChildren<SkinnedMeshRenderer>().Length;x++)
                {
                    GameObject.FindGameObjectWithTag("JuneMask").GetComponentsInChildren<SkinnedMeshRenderer>()[x].enabled = false;
                }                
                stun.GetComponent<Image>().enabled = true;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (!staffOut)
                    {
                        anim.SetLayerWeight(1, 1);
                        anim.SetBool("Staff", true);
                        staffOut = true;
                        hideGun = true;

                    }
                    else
                    {
                        anim.SetBool("Staff", false);
                        staffOut = false;
                        hideGun = false;
                    }
                }
                else
                {
                    anim.SetBool("Staff", false);
                }
            }
            else
                stun.GetComponent<Image>().enabled = false;

            if (!staffOut)
            {                
                if (staffMaterial.GetFloat("_DissolveAmount") >= 1)
                    staff.GetComponent<MeshRenderer>().enabled = false;
                if (staffTimer < 1)
                {
                    staffTimer += (1 * Time.deltaTime);
                }
                else
                    staffTimer = 1;
            }
            else
            {                
                staff.GetComponent<MeshRenderer>().enabled = true;
                if (staffTimer > 0)
                {
                    staffTimer -= (1 * Time.deltaTime);
                }
                else
                    staffTimer = 0;

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (staffOut)
                {
                    if (peaceTimer == 0)
                    {
                        peaceTimer += Time.deltaTime;
                        anim.SetLayerWeight(1, 1);
                        abilityAttractor.transform.position = cursor.transform.position;
                        anim.SetBool("Peace", true);
                        audioSource.PlayOneShot(GetComponent<Audio>().clips[5]);
                    }
                }
            }
            else
            {
                anim.SetBool("Peace", false);
            }
            if (peaceTimer > 0 && peaceTimer < 12)
            {
                stun.GetComponent<Image>().sprite = Resources.Load<Sprite>("StunNaoOk");
                stun.GetComponent<Image>().enabled = true;
                peaceTimer += Time.deltaTime;
            }
            else if (peaceTimer >= 12)
            {
                stun.GetComponent<Image>().sprite = Resources.Load<Sprite>("StunOk");
                peaceTimer = 0;
            }
            if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(1).IsName("Using Staff"))
            {
                for (int x = 1; x < ability1Particles.Length; x++)
                {
                    ability1Particles[x].Emit(1);
                }
            }

        }

    }

    public Vector3 VetorTeclado()
    {
        Vector3 teclado;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;
        teclado.x = x;
        teclado.y = 0;
        teclado.z = z;
        return teclado;
    }
}
