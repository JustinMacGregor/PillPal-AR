﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadSetup()
    {
        SceneManager.LoadScene("2_Setup_PillPal");
    }

    public void LoadDailyPills()
    {
        SceneManager.LoadScene("3_Steps_PillPal");
    }
}