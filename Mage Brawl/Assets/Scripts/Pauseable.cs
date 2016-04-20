using UnityEngine;
using System.Collections;

public abstract class Pauseable : MonoBehaviour {
    private bool paused;
    private static Pauseable[] targs;

	// Use this for initialization
	void Start () {
        targs = Object.FindObjectsOfType<Pauseable>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void pauseGame(bool pausing)
    {
        foreach (Pauseable spot in targs)
            spot.pause(pausing);
    }

    protected virtual void pause(bool pause)
    {
        if (paused == pause)
        {
            return;
        }
        else
            paused = pause;
    }
}
