using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Timing;
using UnityEngine;
using UnityEngine.UI;

public class TimeMachineSlider : MonoBehaviour
{

    public Canvas rulerCanvas;
    public Canvas rulerScrollCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Determine current game tick.
        // GameTick tick = Game.TimeMachine.GetCurrentTick();
        
        // populate the rulers
        Canvas image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
        image = Instantiate(rulerCanvas);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 61);
        image.transform.SetParent(rulerScrollCanvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
