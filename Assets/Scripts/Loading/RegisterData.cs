using TMPro;
using UnityEngine;

namespace Rooms.Multiplayer.Loading
{
    public class RegisterData : MonoBehaviour
    {
        public TMP_InputField Username;
        public TMP_InputField Phone;
        public TMP_InputField Password;
        public TMP_InputField ConfirmPassword;

        public bool Validate()
        {
            return Phone.text.Length <= 11 && Password.text == ConfirmPassword.text;
        }
    }
}