using System;
using SubterfugeCore.Models.GameEvents;
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

        public void loadFromConfig(MapConfiguration configuration)
        {
            disableInput();
            startingOutpostsSlider.value = configuration.OutpostsPerPlayer;
            minOutpostDistanceSlider.value = configuration.MinimumOutpostDistance;
            maxOutpostDistanceSlider.value = configuration.MaximumOutpostDistance;
            dormantsPerPlayerSlider.value = configuration.DormantsPerPlayer;
            factoryWeightSlider.value = configuration.OutpostDistribution.FactoryWeight;
            generatorWeightSlider.value = configuration.OutpostDistribution.GeneratorWeight;
            watchtowerWeightSlider.value = configuration.OutpostDistribution.WatchtowerWeight;
        }

        private void disableInput()
        {
            startingOutpostsSlider.interactable = false;
            minOutpostDistanceSlider.interactable = false;
            maxOutpostDistanceSlider.interactable = false;
            dormantsPerPlayerSlider.interactable = false;
            factoryWeightSlider.interactable = false;
            generatorWeightSlider.interactable = false;
            watchtowerWeightSlider.interactable = false;
            
        }

        public MapConfiguration getConfiguredValues()
        {
            var config = new MapConfiguration()
            {
                DormantsPerPlayer = (int)dormantsPerPlayerSlider.value,
                MaximumOutpostDistance = (int)maxOutpostDistanceSlider.value,
                MinimumOutpostDistance = (int)minOutpostDistanceSlider.value,
                OutpostsPerPlayer = (int)startingOutpostsSlider.value,
                Seed = (int)DateTime.Now.ToFileTimeUtc(),
            };

            var outpostWeights = new OutpostDistribution();

            var watchtowerWeight = Mathf.Round(watchtowerWeightSlider.value * 100f) * 0.01f;
            var factoryWeight = Mathf.Round(factoryWeightSlider.value * 100f) * 0.01f;
            var generatorWeight = Mathf.Round(generatorWeightSlider.value * 100f) * 0.01f;
            var totalWeight = factoryWeight + generatorWeight + watchtowerWeight;

            if (totalWeight < 0.001)
            {
                outpostWeights.FactoryWeight = 0.4f;
                outpostWeights.GeneratorWeight = 0.4f;
                outpostWeights.WatchtowerWeight = 0.2f;
            }
            else
            {
                outpostWeights.FactoryWeight = Math.Abs(factoryWeight) < 0.001 ? 0.0f : Mathf.Round(factoryWeight / totalWeight * 100f) * 0.01f;
                outpostWeights.GeneratorWeight = Math.Abs(generatorWeight) < 0.001 ? 0.0f : Mathf.Round(generatorWeight / totalWeight * 100f) * 0.01f;
                outpostWeights.WatchtowerWeight = Math.Abs(watchtowerWeight) < 0.001 ? 0.0f :Mathf.Round(watchtowerWeight / totalWeight * 100f) * 0.01f;
            }

            config.OutpostDistribution = outpostWeights;
            
            return config;
        }

        // Check if the selected options are valid.
        public void validate()
        {
            
        }

    }
}