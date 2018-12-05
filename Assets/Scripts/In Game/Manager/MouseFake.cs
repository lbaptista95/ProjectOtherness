using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFake : MonoBehaviour
{

    // Use this for initialization
    
    RaycastHit clique;
    GameObject player;

    public void Start()
    {        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Mouse falso para mostrar a posição com o Y ajustado        
        clique = GetPointUnderCursor2();
        player = GameObject.Find("June(Clone)");
        transform.position = new Vector3(clique.point.x, player.GetComponentInChildren<ParticleSystem>().gameObject.transform.position.y, clique.point.z);

    }
    public RaycastHit GetPointUnderCursor2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3 pos = transform.position;
        if (Physics.Raycast(ray, out hit, 2000, 1 << 8))
        {
            Debug.DrawLine(ray.origin, hit.point);
        }
        
        return hit;
    }
}
