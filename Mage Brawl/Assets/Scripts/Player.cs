using UnityEngine;
using System.Collections;

public class Player : Pauseable {

    //Element: 1 = ice, 2 = air, 3 = fire
    public int playerNumber, element, consecutiveHits = 0;
    private bool slowed, paused;
    public float spd = 15f, slowPercent = 70;
    [Tooltip("The Prefab you are shooting")]
    public GameObject iceProjectile, fireProjectile, earthProjectile, aimer;
    private Animator anim;
    private Rigidbody2D rb;
    private float xDir = 0, yDir = 0;
    public AudioClip shoot, hit, superHit;



	// Use this for initialization
	void Start () {
        element = Random.Range(1, 4);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        slowPercent /= 100;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}



    public void freeze(int Duration)
    {
        consecutiveHits++;
        if (slowed)
        {
            CancelInvoke("unfreeze");
            Invoke("unfreeze", Duration);
        }
        else {
            slowed = true;
            Invoke("unfreeze", Duration);
        }
    }

    public void fireSmack()
    {
        consecutiveHits++;
    }

    public void iHit()
    {
        consecutiveHits = 0;
    }

    public void unfreeze()
    {
        slowed = false;
    }

    public void airSmacked(Vector2 Direction, float Force)
    {
        consecutiveHits++;
        rb.AddForce(Direction * Force);
    }


}
