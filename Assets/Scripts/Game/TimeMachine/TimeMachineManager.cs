using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.Game
{
    public class TimeMachineManager : MonoBehaviour
    {
        public TextMeshProUGUI gameTickText;
        public TextMeshProUGUI tickTimeText;
        public Canvas timeMachineGui;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        void Update()
        {
            gameTickText.text = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick().GetTick().ToString();
            tickTimeText.text = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick().GetDate(ApplicationState.currentGameConfig.TimeStarted.ToLocalTime()).ToString(CultureInfo.CurrentCulture);
        }

        public void forwardTick(int amount)
        {
            ApplicationState.CurrentGame.TimeMachine.Advance(amount);
        }

        public void backwardTick(int amount)
        {
            var timeMachine = ApplicationState.CurrentGame.TimeMachine;
            if (timeMachine.GetCurrentTick().GetTick() < amount)
            {
                ApplicationState.CurrentGame.TimeMachine.Rewind(timeMachine.GetCurrentTick().GetTick());
            }
            else
            {
                ApplicationState.CurrentGame.TimeMachine.Rewind(amount);
            }
        }

        public void showTimeMachine()
        {
            timeMachineGui.gameObject.SetActive(true);
        }

        public void hideTimeMachine()
        {
            timeMachineGui.gameObject.SetActive(false);
        }
    }
}