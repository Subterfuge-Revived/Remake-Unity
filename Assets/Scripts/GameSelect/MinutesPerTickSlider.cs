using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.GameSelect
{
    public class MinutesPerTickSlider : MonoBehaviour
    {
        public TextMeshProUGUI textValue;
        public TextMeshProUGUI etaText;

        public void setValue(Slider slider)
        {
            float value = slider.value;
            float interval = 0.1f; //any interval you want to round to
            value = Mathf.Round(value / interval) * interval;
            
            textValue.text = $"MINUTES PER TICK: {value}";
            var eta = ((value * 1000) / 60) / 24;
            float etaRounded = 0.1f; //any interval you want to round to
            etaRounded = Mathf.Round(eta / etaRounded) * etaRounded;
            if (etaRounded < 1)
            {
                etaText.text = $"ETA: {etaRounded * 24}h";
            }
            else
            {
                etaText.text = $"ETA: {etaRounded}d";
            }

            slider.value = value;
        }
    }
}