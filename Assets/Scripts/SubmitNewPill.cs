﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SubmitNewPill : MonoBehaviour
{
    public GameObject pillList;
    TextMeshProUGUI pillListText;
    TextMesh predictionTextMesh;
    int index;
    public string[] pills;

    public UnityEvent goToHome;

    // Start is called before the first frame update
    void Start()
    {
        GameObject predictionGameObject = GameObject.FindGameObjectWithTag("Prediction");
        predictionTextMesh = predictionGameObject.GetComponent<TextMesh>();
        pillListText = pillList.GetComponent<TextMeshProUGUI>();
        pillListText.text = "Hello";

        pills = new string[PlayerPrefs.GetInt("numPillsToTake")];
        Debug.Log(pills.Length);
        index = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        if (index == PlayerPrefs.GetInt("numPillsToTake"))
        {
            //goToHome.Invoke();
            SceneManager.LoadScene("3_Steps_PillPal");
        } else
        {
            if (predictionTextMesh.text != "")
            {
                pills[index] = "Capsule Pill";
                //GlobalPillScreenshotData.instance.pillScreenshotStringData[index] = GetImagePrediction.instance.currentScreenshotImageString;
                PlayerPrefs.SetString("pillToTake_" + index, GetImagePrediction.instance.currentScreenshotImageString);
                PlayerPrefs.Save();
                index++;
                int tempIndex = 1;
                string tempTextMeshString = "";

                foreach (string pill in pills)
                {
                    if (pill != null)
                    {

                        string indexFormatted = "<size=32><b>" + tempIndex.ToString() + ".  </b></size>";
                        tempTextMeshString = tempTextMeshString + indexFormatted;
                        tempTextMeshString = tempTextMeshString + pill.ToString();
                        tempTextMeshString = tempTextMeshString + "\n";
                        tempIndex++;
                    }
                }

                pillListText.text = tempTextMeshString;
                predictionTextMesh.text = "";
            }
        }
    }
}
