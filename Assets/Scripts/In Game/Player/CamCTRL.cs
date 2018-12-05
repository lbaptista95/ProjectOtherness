using UnityEngine;

public class CamCTRL : MonoBehaviour
{

    public float panSpeed = 10f;
    public float panBorder = 10f;
    public GameObject target;
    public float smoothSpeed = 0.08f;
    public bool focus;
    public Vector3 offset;


    private void Start()
    {
        
    }

    void Update()
    {

        target = GameObject.Find("June(Clone)");
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorder)
        {
            pos.z += panSpeed * Time.deltaTime;
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= panBorder)
        {
            pos.z -= panSpeed * Time.deltaTime;
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - panBorder)
        {
            pos.z -= panSpeed * Time.deltaTime;
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= panBorder)
        {
            pos.z += panSpeed * Time.deltaTime;
            pos.x -= panSpeed * Time.deltaTime;
        }
        transform.position = pos;

        this.gameObject.transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime);        

        transform.Translate(Vector3.back * smoothSpeed * Time.deltaTime);




    }

}
