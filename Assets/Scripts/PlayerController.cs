using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float strafeSpeed = 5;

    Camera cam;
    Rigidbody2D rb2D;

	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        
	}
	void  Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()

    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rb2D.angularVelocity = 0;

        float inputVert = Input.GetAxisRaw("Vertical");
        float inputHor = Input.GetAxisRaw("Horizontal");

        rb2D.velocity = new Vector3(inputHor * speed, inputVert * speed);

    }  
}
