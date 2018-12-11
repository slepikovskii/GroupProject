using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Artem
public class Sound : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        
            AudioSource source = GetComponent<AudioSource>();
            source.Play();

        
    }

}
