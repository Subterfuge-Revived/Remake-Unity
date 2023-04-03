using UnityEngine;
using UnityEngine.EventSystems;

namespace Rooms.Multiplayer.Game
{
    public class TabGuiController : MonoBehaviour
    {
        public GameObject chatWindow;
        public GameObject eventWindow;
        public GameObject statsWindow;
        public GameObject logWindow;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Determine if the click was in the panel
                if (!EventSystem.current.IsPointerOverGameObject()) disableAll();
            }
        }
        
        public void showChat()
        {
            disableAll();
            chatWindow.SetActive(true);
        }
        
        public void showEventWindow()
        {
            disableAll();
            eventWindow.SetActive(true);
        }
        
        public void showStatsWindow()
        {
            disableAll();
            statsWindow.SetActive(true);
        }
        
        public void showLogWindow()
        {
            disableAll();
            logWindow.SetActive(true);
        }

        public void disableAll()
        {
            chatWindow.SetActive(false);
            eventWindow.SetActive(false);
            statsWindow.SetActive(false);
            logWindow.SetActive(false);
        }
        
    }
}