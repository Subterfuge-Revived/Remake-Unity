using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.CreateGame
{
    public class OutpostWeightDisplayController : MonoBehaviour
    {
        public GeneralConfigController GeneralConfigController;
        public MapConfigController MapConfigController;
        public OutpostWeightDisplay OutpostWeightDisplay;

        public void updateOutpostCounts()
        {
            var totalOutposts = (int)GeneralConfigController.playerCountSlider.value * 
                                ((int)MapConfigController.dormantsPerPlayerSlider.value + (int)MapConfigController.startingOutpostsSlider.value);
            var factoryCount = (int)Math.Round(totalOutposts * MapConfigController.getConfiguredValues().FactoryWeight);
            var generatorCount = (int)Math.Round(totalOutposts * MapConfigController.getConfiguredValues().GeneratorWeight);
            var watchtowerCount = (int)Math.Round(totalOutposts * MapConfigController.getConfiguredValues().WatchtowerWeight);
            
            OutpostWeightDisplay.setTotalFactories(factoryCount);
            OutpostWeightDisplay.setTotalGenerators(generatorCount);
            OutpostWeightDisplay.setTotalWatchtowers(watchtowerCount);
            OutpostWeightDisplay.setTotalOutposts(totalOutposts);
        }
        
    }
}