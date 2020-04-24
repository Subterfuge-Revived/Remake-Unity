using System.Collections;
using System.Collections.Generic;
using Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StringFactory.LoadStrings();
        SceneManager.LoadScene("LoginScreen");
    }
}
