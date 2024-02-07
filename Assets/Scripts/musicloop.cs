using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicloop : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicStart;
    //public PlayerHealth player;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        //dead = player.died;
        musicSource.PlayOneShot(musicStart);
        //musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }


    // Update is called once per frame
    void Update()
    {

        if(dead)
        {
            Dead();
        } 
    }

    void Dead()
    {
        musicSource.Stop();
    }
}
