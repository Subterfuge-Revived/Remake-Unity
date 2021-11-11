using TMPro;
using UnityEngine;

namespace Rooms.Multiplayer.CreateGame
{
    public class OutpostWeightDisplay : MonoBehaviour
    {
        public TextMeshProUGUI totalOutposts;
        public TextMeshProUGUI factoryCount;
        public TextMeshProUGUI generatorCount;
        public TextMeshProUGUI watchtowerCount;

        public void setTotalOutposts(int count)
        {
            totalOutposts.text = count.ToString();
        }
        
        public void setTotalFactories(int count)
        {
            factoryCount.text = count.ToString();
        }
        
        public void setTotalGenerators(int count)
        {
            generatorCount.text = count.ToString();
        }
        
        public void setTotalWatchtowers(int count)
        {
            watchtowerCount.text = count.ToString();
        }
        
    }
}