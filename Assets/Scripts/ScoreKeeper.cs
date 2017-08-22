using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    public static int ScorePoints;
    private Text myText;


    private void Start()
    {
        myText=GetComponent<Text>();
        ResetScore();
    }


    public void Score(int points)
    {
        ScorePoints += points;
        myText.text = ScorePoints.ToString();
    }
    
    public static void ResetScore()
    {
        ScorePoints = 0;
    }
    
}
