using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
    

public class Current_Time_UI_Script : MonoBehaviour
{

    TextMeshProUGUI currentTime;
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = this.GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime.text = "Goal : " + gameManager.GetComponent<Game_Engine>().endScore + "\n" + gameManager.GetComponent<Game_Engine>().currentTime.ToString("00:00");
    }
}
