using Translation;
using UnityEngine;
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
