﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Ceci
public class MainMenuScript : MonoBehaviour
{

    public void StarttheGame()
    {

        SceneManager.LoadScene(1);


    }

    public void QuittheGame()
    {

        Application.Quit();
    }


}

