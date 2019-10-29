using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blue_Rally_UI_Script : MonoBehaviour
{
    public TextMeshProUGUI numOfResourceTextBox;

    public GameObject gameManager;
    float bluecurrentNumOfResource = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        bluecurrentNumOfResource = gameManager.GetComponent<Game_Engine>().numOfBlueResourceTotal;

        numOfResourceTextBox.text = bluecurrentNumOfResource.ToString("0");
    }
}
