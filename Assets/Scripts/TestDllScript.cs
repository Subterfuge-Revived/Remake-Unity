using UnityEngine;
using SubterfugeCore.Core;
using UnityEngine.UI;

public class TestDllScript : MonoBehaviour
{
    public Text textElement;
    
    // Start is called before the first frame update
    void Start()
    {   
        Game game = new Game();
        Debug.Log("There are " + Game.timeMachine.getState().getOutposts().Count.ToString() + " outposts.");
    }

    // Update is called once per frame
    void Update()
    {
        Game.timeMachine.advance(1);
        Debug.Log("Current tick: " + Game.timeMachine.getCurrentTick().getTick().ToString());
        textElement.text = "Current Tick: " + Game.timeMachine.getCurrentTick().getTick().ToString();
    }
}
