using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Red_Rally_UI_Scrpit : MonoBehaviour
{
    public TextMeshProUGUI numOfResourceTextBox;

    public GameObject gameManager;
    float redcurrentNumOfResource = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        redcurrentNumOfResource = gameManager.GetComponent<Game_Engine>().numOfRedResourceTotal;

        numOfResourceTextBox.text = redcurrentNumOfResource.ToString("0");
    }
}
