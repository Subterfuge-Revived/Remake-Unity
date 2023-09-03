using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer
{
    public class ColorChange : MonoBehaviour {
        public Color[] colorsToCycle;
        public Image imageToAlter;
         
        public int currentIndex = 0;
        private int nextIndex;
     
        public float changeColourTime = 2.0f;
     
        private float lastChange = 0.0f;
        private float timer = 0.0f;
     
        void Start() {
            if (colorsToCycle == null || colorsToCycle.Length < 2)
                Debug.LogError("Need to setup colors array in inspector");
         
            nextIndex = (currentIndex + 1) % colorsToCycle.Length;    
        }
     
        void Update() {
         
            timer += Time.deltaTime;
         
            if (timer > changeColourTime) {
                currentIndex = (currentIndex + 1) % colorsToCycle.Length;
                nextIndex = (currentIndex + 1) % colorsToCycle.Length;
                timer = 0.0f;
             
            }
            imageToAlter.color = Color.Lerp (colorsToCycle[currentIndex], colorsToCycle[nextIndex], timer / changeColourTime );
        }
    }
}