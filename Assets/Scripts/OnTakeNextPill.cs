using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTakeNextPill : MonoBehaviour
{
    [SerializeField] private TextMeshPro buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnNextPill()
    {
        if (GlobalPillTakingLogic.instance.currentPillIndex + 1 < PlayerPrefs.GetInt("numPillsToTake"))
        {
            GlobalPillTakingLogic.instance.currentPillIndex++;
        }
        else
        {
            GlobalPillTakingLogic.instance.currentPillIndex++;
            if (buttonText.text != "Finish")
            {
                buttonText.text = "Finish";
            }
            else
            {
                PlayerPrefs.SetInt("pillsTakenTodayBool", 1);
                SceneManager.LoadScene("1_Menu_PillPal");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
