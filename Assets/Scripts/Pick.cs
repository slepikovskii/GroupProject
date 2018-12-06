using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{

    public AudioSource Select;
    // Use this for initialization
    void Start()
    {
        Select = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* void OnTriggerEnter(Collider colider)
     {
     if (colider.gameObject.tag == "Player")
     {

             Destroy(this.gameObject);
             Select.Play();
     }
     }*/
}
