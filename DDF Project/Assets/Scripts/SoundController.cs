using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

	public AudioSource asound;
	public Slider sd;
    private float musicVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        asound.Play();
        musicVolume = PlayerPrefs.GetFloat("volume");
        asound.volume = musicVolume;
        sd.value = musicVolume;        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(asound.clip.name);       
        asound.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
        
    }

    public void updateVolume(float volume)
    {
    	musicVolume = volume;
    }


}
