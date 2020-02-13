using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Api : MonoBehaviour
{

    public void TryLogin()
    {
        InputField[] inputFields = FindObjectsOfType<InputField>();
        Debug.Log(inputFields.Length.ToString());

        string password = "";
        string username = "";
        
        // Get form inputs
        foreach (InputField field in inputFields)
        {
            if (field.name == "PasswordInput")
            {
                password = field.text;
            }
            
            if (field.name == "UsernameInput")
            {
                username = field.text;
            }
        }

        StartCoroutine(sendLoginRequest(username, password));

        Debug.Log(username);
        Debug.Log(password);
    }

    private IEnumerator sendLoginRequest(string username, string password)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username=" + username + "&password=" + password));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/subterfuge-remake/login.php", formData);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Login success");
        }
    }
}
