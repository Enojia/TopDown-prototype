using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public int health = 100;
    public int strength = 10;
    public bool isArmed = false;
    public float fireRate = 4.0f;
    public float nextFire = 0.0f;
    public GameObject Bullet;
    public AudioSource ShotSfx;


    public enum State
    { Wounded, Ground, Dead };

    Camera cam;
    Rigidbody2D rb2D;
    Animator anim;

	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        anim = GetComponentInChildren<Animator>();
        
        
	}
	void  Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        if(Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
            anim.SetTrigger("Shoot");
            ShotSfx.Play();
        }
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

    public void Shoot()
    {
        
    }
}
