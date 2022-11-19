﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GetImagePrediction : MonoBehaviour
{
    // INITIALIZE CLARIFAI VARIABLES //
    private string USER_ID = "justingg";
    private string PAT = "03e4d15f3e074dd09eb2d7e5dade2814";
    private string APP_ID = "pillpal";
    private string MODEL_ID = "pills";
    private string MODEL_VERSION_ID = "d869d62602094986930528bc00f0beaa";

    // Start is called before the first frame update
    void Start()
    {
    }

    public void TakeScreenshot()
    {
        //will be called in TakeScreenshot
    }



    //The method that begins the prediction process
    //Takes Image as byte64 string
    public void MakePrediction(string IMAGE_BYTES_STRING)
    {
        string body = @"{ user_app_id: { user_id" + USER_ID + ", app_id: " + APP_ID + "}, inputs: [ { data: { image: { base64: " + IMAGE_BYTES_STRING + " } } } ] }";
        var BASE_URL = "https://api.clarifai.com/v2/models/" + MODEL_ID + "/versions/" + MODEL_VERSION_ID + "/outputs";

        var uwr = new UnityWebRequest(BASE_URL, UnityWebRequest.kHttpVerbPOST);
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(body);
        Debug.Log("Body data: " + bodyData.ToString());

        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyData);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        uwr.SetRequestHeader("Accept", "application/json");
        uwr.SetRequestHeader("Authorization", "Key " + PAT);
        uwr.SetRequestHeader("Content-Type", "application/json");

        //And we start a new co routine in Unity and wait for the response.
        StartCoroutine(WaitForRequest(uwr, bodyData, BASE_URL));
    }

 
    //Wait for the www Request and get result
    IEnumerator WaitForRequest(UnityWebRequest uwr, byte[] bodyData, string BASE_URL)
    {
        WWWForm form = new WWWForm();
        form.AddField("body", bodyData.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.responseCode);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}