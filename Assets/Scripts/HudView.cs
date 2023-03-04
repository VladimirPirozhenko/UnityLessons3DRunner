using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudView : BaseView
{
    [SerializeField] private TextMeshProUGUI score; 
 
    void Start()
    {
        score.text = "Score: 0";
    }
    // Update is called once per frame
    public void UpdateScore(int newScore)
    {
        score.text = "Score: " + newScore.ToString();
    }
}
