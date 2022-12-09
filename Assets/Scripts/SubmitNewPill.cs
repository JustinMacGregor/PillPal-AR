using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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

        pills = new string[GlobalState.instance.numPills];
        Debug.Log(pills.Length);
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        if (index == GlobalState.instance.numPills)
        {
            goToHome.Invoke();
        } else
        {
            if (predictionTextMesh.text != "")
            {
                pills[index] = "Capsule Pill";
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
