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
            Text textElement = this.GetComponent<Text>();
            textElement.text = StringFactory.GetString(textToTranslate);
        }
    }
}