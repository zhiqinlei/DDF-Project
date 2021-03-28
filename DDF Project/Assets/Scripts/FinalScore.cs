using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    // show final socre on end menu
    public Text scoreText;
    public int scoreAmount;
    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = Score.GetScore();
        scoreText.text = "Final Score: " + scoreAmount + " S";
        
    }

}
