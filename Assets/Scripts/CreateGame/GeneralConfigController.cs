using System.Net.Configuration;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.CreateGame
{
    public class GeneralConfigController : MonoBehaviour
    {
        public Toggle rankedToggle;
        public Toggle anonToggle;
        public Toggle miningToggle;
        public Toggle dominationToggle;
        public Slider playerCountSlider;
        public Slider minutesPerTickSlider;

        public void loadFromConfig(GameSettings config)
        {
            disableInput();
            rankedToggle.isOn = config.IsRanked;
            anonToggle.isOn = config.Anonymous;
            miningToggle.isOn = config.Goal == Goal.Mining;
            dominationToggle.isOn = config.Goal == Goal.Domination;
            playerCountSlider.value = config.MaxPlayers;
            minutesPerTickSlider.value = (float)config.MinutesPerTick;
        }

        public void disableInput()
        {
            rankedToggle.interactable = false;
            anonToggle.interactable = false;
            miningToggle.interactable = false;
            dominationToggle.interactable = false;
            playerCountSlider.interactable = false;
            minutesPerTickSlider.interactable = false;
        }

        public GameSettings getConfiguredValues()
        {
            var config = new GameSettings();
            config.Anonymous = anonToggle.isOn;
            config.IsRanked = rankedToggle.isOn;
            config.Goal = miningToggle.isOn ? Goal.Mining : Goal.Domination;
            config.MaxPlayers = (int)playerCountSlider.value;
            config.MinutesPerTick = minutesPerTickSlider.value;
            return config;
        }

        // Check if the selected options are valid.
        public void validate()
        {
            
        }

    }
}

public class GeneralConfig
{
    public bool IsRanked { get; set; } = true;
    public bool IsAnonymous { get; set; } = false;
    public Goal Goal { get; set; }= Goal.Mining;
    public int MaxPlayers { get; set; } = 6;
    public double MinutesPerTick { get; set; }= 10.0;
}