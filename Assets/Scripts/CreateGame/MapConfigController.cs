using System;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.CreateGame
{
    public class MapConfigController : MonoBehaviour
    {
        public Slider startingOutpostsSlider;
        public Slider minOutpostDistanceSlider;
        public Slider maxOutpostDistanceSlider;
        public Slider dormantsPerPlayerSlider;
        
        public Slider factoryWeightSlider;
        public Slider generatorWeightSlider;
        public Slider watchtowerWeightSlider;

        public MapConfig getConfiguredValues()
        {
            var config = new MapConfig();
            config.StartingOutposts = (int)startingOutpostsSlider.value;
            config.MinOutpostDistance = (int)minOutpostDistanceSlider.value;
            config.MaxOutpostDistance = (int)maxOutpostDistanceSlider.value;
            config.DormantsPerPlayer = (int)dormantsPerPlayerSlider.value;

            var watchtowerWeight = Mathf.Round(watchtowerWeightSlider.value * 100f) * 0.01f;
            var factoryWeight = Mathf.Round(factoryWeightSlider.value * 100f) * 0.01f;
            var generatorWeight = Mathf.Round(generatorWeightSlider.value * 100f) * 0.01f;
            var totalWeight = factoryWeight + generatorWeight + watchtowerWeight;

            if (totalWeight < 0.001)
            {
                config.FactoryWeight = 0.4;
                config.GeneratorWeight = 0.4;
                config.WatchtowerWeight = 0.2;
            }
            else
            {
                config.FactoryWeight = Math.Abs(factoryWeight) < 0.001 ? 0.0 : Mathf.Round(factoryWeight / totalWeight * 100f) * 0.01f;
                config.GeneratorWeight = Math.Abs(generatorWeight) < 0.001 ? 0.0 : Mathf.Round(generatorWeight / totalWeight * 100f) * 0.01f;
                config.WatchtowerWeight = Math.Abs(watchtowerWeight) < 0.001 ? 0.0 :Mathf.Round(watchtowerWeight / totalWeight * 100f) * 0.01f;
            }
            
            
            return config;
        }

        // Check if the selected options are valid.
        public void validate()
        {
            
        }

    }
}

public class MapConfig
{
    public int StartingOutposts { get; set; } = 4;
    public int MinOutpostDistance { get; set; } = 120;
    public int MaxOutpostDistance { get; set; } = 600;
    public int DormantsPerPlayer { get; set; } = 6;
    public double FactoryWeight { get; set; } = 0.4;
    public double GeneratorWeight { get; set; } = 0.4;
    public double WatchtowerWeight { get; set; } = 0.4;
}