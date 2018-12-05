using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AlternarControles : MonoBehaviour
{

    // Use this for initialization
    public ControleTeclado controleComum;
    public ControleMouse controleCombate;
    Animator anim;
    GameObject gun;
    GameObject mouse;
    GameObject gameManager;
    public bool interact;
    public bool askingToInteract;
    public GameObject gameMessage;
    public string interactionType;
    public bool chating;
    public bool mission;
    void Awake()
    {
        gun = GameObject.Find("JuneGun");
        gun.GetComponent<Renderer>().enabled = false;
        controleComum = GetComponent<ControleTeclado>();
        controleCombate = GetComponent<ControleMouse>();
        controleComum.enabled = true;
        controleCombate.enabled = false;
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load("JuneComum") as RuntimeAnimatorController;
        mouse = GameObject.FindGameObjectWithTag("Cursor");
        mouse.GetComponent<Renderer>().enabled = false;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameMessage = GameObject.FindGameObjectWithTag("GameMessage");
        mission = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se há algum interação disponível para o player
        if (askingToInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interact = true;
                gameMessage.GetComponent<Text>().text = "";
                askingToInteract = false;

            }
            else
            {
                interact = false;
            }

        }
        else
            interact = false;
        if (anim.runtimeAnimatorController.name == "JuneComum")
        {
            if (GetComponent<ControleTeclado>().staffOut)
                mouse.GetComponent<Renderer>().enabled = true;
            else
                mouse.GetComponent<Renderer>().enabled = false;
        }
        else
            mouse.GetComponent<Renderer>().enabled = true;
        //Caso jogador não esteja conversando com algum personagem
        if (!chating)
        {
            //Alternar de controle comum para o controle com a arma sacada
            if (Input.GetMouseButton(1))
            {
                if (SceneManager.GetActiveScene().name != "Level Design - QG Girafas")
                {
                    anim.runtimeAnimatorController = Resources.Load("JuneCombate") as RuntimeAnimatorController;
                    controleComum.enabled = false;
                    controleCombate.enabled = true;
                }
            }
            else
            {
                anim.runtimeAnimatorController = Resources.Load("JuneComum") as RuntimeAnimatorController;
                controleCombate.enabled = false;
                controleComum.enabled = true;
            }
        }
        if ((anim.GetCurrentAnimatorStateInfo(1).IsName("Gun") && anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.17f) ||
            (anim.GetCurrentAnimatorStateInfo(0).IsName("Butt") && anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.2f))
        {
            gun.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gun.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("BossShot"))
            gameManager.GetComponent<GameManager>().TakeDamage(20);
    }

    //Quando há interação disponível, este método mostrará a mensagem
    public void InteractionAvailable(string interaction)
    {
        interactionType = interaction;
        gameMessage.GetComponent<Text>().text = "Press E to " + interaction;
        askingToInteract = true;
    }

    //Quando não houver mais interação disponível, este método limpará a tela
    public void ClearInteractions()
    {        
        gameMessage.GetComponent<Text>().text = "";
        askingToInteract = false;
    }
}
