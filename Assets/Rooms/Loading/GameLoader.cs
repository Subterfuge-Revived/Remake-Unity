using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using SubterfugeCore.Core.Network;
using SubterfugeRemakeService;
using Translation;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        // Load language strings.
        StringFactory.LoadStrings();

        // Go to login screen.
        SceneManager.LoadScene("MainMenu");
    }
}
