using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Musicscript : MonoBehaviour
{
    public AudioClip MusicClip;
    public AudioSource backgroundmusic;
    public AudioSource paperaudio;
    public AudioSource glassaudio;
    public AudioSource plasticaudio;

    // Use this for initialization
    public void PlayBackground()
    {
        backgroundmusic.playOnAwake = true;
    }
   /* public void SoundEffects()
    {
    
        if (Input.GetKeyDown(KeyCode.E) )
        {
           



            if (gameObject.CompareTag("paperpickup"))
            {

                paperaudio.Play();

            }
            if (gameObject.CompareTag("glasspickup"))
            {
                glassaudio.Play();



            }
            if (gameObject.CompareTag("plasticpickup"))
            {
                plasticaudio.Play();


            }
        }
    }*/
}