using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Rooms.Multiplayer.Game
{
    public class TimeControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public float timeCheckThreshold = 0.5f;
        public bool advanceTimeMachine = false;
        
        private float timePressed = 0.0f;
        private float timeLastPress = 0.0f;
        private bool pressed = false;

        void Update()
        {
            if (pressed)
            {
                // If the user puts their finger on screen...
                timePressed = Time.time - timeLastPress;

                if (timePressed > timeCheckThreshold)
                {
                    timeLastPress = Time.time;
                    // Restart the timer:
                    timePressed = Time.time - timeLastPress;

                    if (timeCheckThreshold <= 0.05)
                    {
                        if (advanceTimeMachine)
                        {
                            ApplicationState.CurrentGame.TimeMachine.Advance(2);
                        }
                        else
                        {
                            ApplicationState.CurrentGame.TimeMachine.Rewind(2);
                        }
                    }
                    else
                    {
                        // Increment the tick and make the next search time slower.
                        timeCheckThreshold -= 0.08f;
                        if (advanceTimeMachine)
                        {
                            ApplicationState.CurrentGame.TimeMachine.Advance(1);
                        }
                        else
                        {
                            ApplicationState.CurrentGame.TimeMachine.Rewind(1);
                        }
                    }
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            timePressed = Time.time;
            pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // If the user raises her finger from screen
            timeLastPress = Time.time;
            timeCheckThreshold = 0.5f;
            pressed = false;
        }
    }
}