using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public float fireRate;
    public float nextFire;

    public AudioSource ShotSfx;
    public GameObject Bullet;
    public GameObject Player;

    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public IEnumerator Move()
    {
        
    }

    public void Shot()
    {

    }
}
