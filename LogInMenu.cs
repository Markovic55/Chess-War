﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class LogInMenu : MonoBehaviour
{

    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;


    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);

       UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);
        yield return www.SendWebRequest();


        if (www.downloadHandler.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.downloadHandler.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }


    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);

    }

}
