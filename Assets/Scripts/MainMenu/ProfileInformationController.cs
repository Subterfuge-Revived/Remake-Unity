using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileInformationController : MonoBehaviour
{

    public TextMeshProUGUI playerInformation;

    public void loadPlayerInformation()
    {
        playerInformation.text = $"You are logged in. Welcome {ApplicationState.player.GetPlayerName()}";
    }
}
