using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Betül 
public class TimerLinear : MonoBehaviour {

    Image FillImg;
    float TimeAmnt = 200;
    float time;


    void Start()
    {

        FillImg = this.GetComponent<Image>();
        time = TimeAmnt;

    }

   
   

    // Update is called once per frame
    void Update () {

        if (time > 0)
        {

            time -= Time.deltaTime;
            FillImg.fillAmount = time / TimeAmnt;

        }
        else
        {
            SceneManager.LoadScene(3);
        }
        

       
    }


   
}
