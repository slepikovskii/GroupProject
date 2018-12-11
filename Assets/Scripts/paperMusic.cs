using UnityEngine;
using System.Collections;
//Svyatoslav
public class paperMusic : MonoBehaviour
{
   
    public AudioSource putaudiosource;
    public AudioSource paperaudio;
    public AudioSource glassaudio;
    public AudioSource plasticaudio;


    public void Sounds()
    {
        if (Input.GetKey(KeyCode.E) && gameObject.CompareTag("paperpickup"))
        {
           
            paperaudio.Play();
        }
        if (Input.GetKey(KeyCode.E) && gameObject.CompareTag("glasspickup"))
        {
            
            glassaudio.Play();
        }
        if (Input.GetKey(KeyCode.E) && gameObject.CompareTag("plasticpickup"))
        {
           
            plasticaudio.Play();
        }
        if (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.M))
        {
           
            putaudiosource.Play();
        }
    }

}
