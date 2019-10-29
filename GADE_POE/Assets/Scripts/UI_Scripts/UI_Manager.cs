using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject startMenu;
    public GameObject inGameMenu;
    public GameObject endGameText;

    float currentBlueScore = 0;
    float currentRedScore = 0;

    float endGoal = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        endGoal = gameManager.GetComponent<Game_Engine>().endScore;
        inGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.GetComponent<Game_Engine>().startGame == true)
        {
            startMenu.SetActive(false);
            inGameMenu.SetActive(true);
            endGameText.SetActive(false);

            UpdateTeamProgressBar();
        }
        if (gameManager.GetComponent<Game_Engine>().endGame == true)
        {
            endGameText.SetActive(true);
        }
        if (gameManager.GetComponent<Game_Engine>().mainMenu == true)
        {
            inGameMenu.SetActive(false);
            endGameText.SetActive(false);
            startMenu.SetActive(true);
        }

    }


    void UpdateTeamProgressBar()
    {
        Image blueTeamBar = GameObject.FindGameObjectWithTag("Blue Team Progress Bar").GetComponent<Image>();
        Image redTeamBar = GameObject.FindGameObjectWithTag("Red Team Progress Bar").GetComponent<Image>();

        currentBlueScore = gameManager.GetComponent<Game_Engine>().currentBlueTeamScore;
        currentRedScore = gameManager.GetComponent<Game_Engine>().currentRedTeamScore;

        blueTeamBar.fillAmount = currentBlueScore / endGoal;
        redTeamBar.fillAmount = currentRedScore / endGoal;
    }
}
