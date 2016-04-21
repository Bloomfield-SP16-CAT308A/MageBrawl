using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Pauseable  {
    Player maker;
    Rigidbody2D rb;
    public int Score, freezeDuration;
    private int element;
    private GameObject papa;
    public float knockBack;
    private bool paused;
    private Vector2 tempVel;
    private static readonly Vector2 sentinel = Vector2.zero;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(paused)
            if(rb.velocity != sentinel)
            {
                tempVel = rb.velocity;
                rb.velocity = Vector2.zero;
            }
	}

    public void birthBud(Player make, int elemen, float force)
    {
        maker = make;
        element = elemen;
        rb.velocity = (force * transform.right.normalized);
        papa = make.gameObject;
    }

    //Element: 1 = ice, 2 = air, 3 = fire
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && papa != other.gameObject)
        {
            switch (element)
            {
                case 1: GameController.gameController.players[(maker.playerNumber + 1) % 2].freeze(freezeDuration);
                    GameController.addScore(maker.playerNumber, Score);
                    break;
                case 2: GameController.gameController.players[(maker.playerNumber + 1) % 2].airSmacked(new Vector2(transform.rotation.x,transform.rotation.y), knockBack);
                    GameController.addScore(maker.playerNumber, Score);
                    break;
                case 3: GameController.gameController.players[(maker.playerNumber + 1) % 2].fireSmack();
                    GameController.addScore(maker.playerNumber, Score);
                    break;
                default: Debug.Log("Error, no element");
                    break;
            }            
        }
        Destroy(this.gameObject);
    }

    protected override void pause(bool pause)
    {
        if (paused == pause)
        {
            return;
        }
        else if(pause == true)
        {
            rb.velocity = tempVel;
            paused = pause;
            tempVel.Set(sentinel.x, sentinel.y);
        }
        else
            paused = pause;
    }

}
