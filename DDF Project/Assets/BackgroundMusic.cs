using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private static BackgroundMusic backgroundMusic;
    void Awake()
    {
    	if(backgroundMusic == null) 
    	{
    		backgroundMusic = this;
    		DontDestroyOnLoad(backgroundMusic);
    	}

    	else 
    	{
    		Destroy(gameObject);
    	}
        
    }
}
