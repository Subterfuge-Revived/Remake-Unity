using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.GameSelect
{
    public class SliderTextUpdater : MonoBehaviour
    {
        public TextMeshProUGUI textValue;
        public String textPrefix;

        public void setValue(Slider slider)
        {
            var roundedVal = Mathf.Round(slider.value * 100f) * 0.01f;
            textValue.text = $"{textPrefix}: {roundedVal}";
        }
    }
}