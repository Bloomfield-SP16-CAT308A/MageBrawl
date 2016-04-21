using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : Pauseable, IComparer<Player>{

    //Element: 1 = ice, 2 = air, 3 = fire
    public int playerNumber, element, consecutiveHits = 0;
    private bool slowed, paused;
    public float spd = 15f, slowPercent = 70f, projectileSpeed = 100f;
    public readonly float fireCD = 2f;
    [Tooltip("The Prefab you are shooting")]
    public GameObject iceProjectile, fireProjectile, airProjectile, aimer;
    private Animator anim;
    private Rigidbody2D rb;
    private float xDir = 0, yDir = 0, xAim = 0, yAim = 0, currentCD;
    public AudioClip shoot, hit, superHit;
    private GameObject myProjectile;
    private Vector2 tempVel, sentinel = new Vector2(Mathf.PI, Mathf.PI);



    // Use this for initialization
    void Start () {
        tempVel = sentinel;
        element = Random.Range(1, 4);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        slowPercent /= 100;
        switch (element)
        {
            case 1: myProjectile = iceProjectile;
                break;
            case 2: myProjectile = airProjectile;
                break;
            case 3: myProjectile = fireProjectile;
                break;
            default: myProjectile = iceProjectile;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            if (rb.velocity != sentinel)
            {
                tempVel = rb.velocity;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            movePlayer();
            if (Input.GetButtonDown("Fire" + playerNumber))
                fire();
        }
    }



    public int Compare(Player a, Player b)
    {
        return a.playerNumber - b.playerNumber;
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

    private void movePlayer()
    {
        yDir = Input.GetAxis("Vertical" + playerNumber);
        xDir = Input.GetAxis("Horizontal" + playerNumber);
        if (slowed)
            rb.velocity = new Vector2(xDir, yDir) * spd * slowPercent;
        else
            rb.velocity = new Vector2(xDir, yDir) * spd;
        xAim = Input.GetAxis("RightHorizontal" + playerNumber);
        yAim = Input.GetAxis("RightVertical" + playerNumber);
        float rotationDegrees = Mathf.Atan2(yAim, xAim);
        gameObject.transform.rotation = Quaternion.Euler(0f, 0, rotationDegrees); 

    }

    private void fire()
    {
        if (currentCD <= 0f)
        {
            GameObject projectile = Instantiate(myProjectile, aimer.transform.position, aimer.transform.rotation) as GameObject;
            projectile.GetComponent<Projectile>().birthBud(this, element, projectileSpeed);
            currentCD = fireCD;
        }

    }

    protected override void pause(bool pause)
    {
        if (paused == pause)
        {
            return;
        }
        else if (pause == true)
        {
            rb.velocity = tempVel;
            paused = pause;
            tempVel.Set(sentinel.x, sentinel.y);
        }
        else
            paused = pause;
    }


}
