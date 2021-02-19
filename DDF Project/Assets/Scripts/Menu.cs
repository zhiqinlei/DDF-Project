﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame ()
    {
    	SceneManager.LoadScene ("Game");
    }

    public void QuitGame ()
    {
    	Debug.Log("QUIT!");
    	Application.Quit();
    }

    public void EnableUI()
    {
    	GameObject.Find("Canvas/menu/UI").SetActive(true);
    }
}
