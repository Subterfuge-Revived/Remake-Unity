using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Translation
{
    /// <summary>
    /// This script must be attached to a Text component!
    /// </summary>
    public class Translator : MonoBehaviour
    {
        /// <summary>
        /// The text to translate for display in the game.
        /// </summary>
        public GameString textToTranslate;
        
        public void Start()
        {
            TextMeshProUGUI textMesh = this.GetComponent<TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh.text = StringFactory.GetString(textToTranslate);
            }
            Text text = this.GetComponent<Text>();
            if (text != null)
            {
                text.text = StringFactory.GetString(textToTranslate);
            }
            TextMeshPro textMeshPro = this.GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                textMeshPro.text = StringFactory.GetString(textToTranslate);
            }
        }
    }
}