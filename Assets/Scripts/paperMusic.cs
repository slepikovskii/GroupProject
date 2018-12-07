using UnityEngine;
using System.Collections;

public class paperMusic : MonoBehaviour
{
    public AudioClip audioclip1;
    public AudioClip audioclip2;
    public AudioClip audioclip3;
    public AudioSource paperaudio;
    public AudioSource glassaudio;
    public AudioSource plasticaudio;

    public void Sounds()
    {
        if (Input.GetKey(KeyCode.E) && gameObject.CompareTag("paperpickup"))
        {
            paperaudio.clip = audioclip1;
            paperaudio.Play();
        }
        if (Input.GetKey(KeyCode.E) && gameObject.CompareTag("glasspickup"))
        {
            glassaudio.clip = audioclip2;
            glassaudio.Play();
        }
        if (Input.GetKey(KeyCode.E) && gameObject.CompareTag("plasticpickup"))
        {
            plasticaudio.clip = audioclip3;
            plasticaudio.Play();
        }
    }

}