using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TrackNameDisplay : MonoBehaviour
{

    public Scrollbar horizonalScrollBar;
    public TextMeshProUGUI textDisplay;
    
    
    private float currentScroll = 0.0f;

    private int waitTimer = 0;
    private int scrollDelay = 100;
    private ScrollState scrollState = ScrollState.WAIT_START;
    
    private enum ScrollState
    {
        SCROLLING,
        WAIT_END,
        WAIT_START,
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        textDisplay.text = AudioPlayer.getSongName();
        
        if (scrollState == ScrollState.WAIT_START)
        {
            delayStart();
        }
        if (scrollState == ScrollState.SCROLLING)
        {
            Scroll();
        }
        if (scrollState == ScrollState.WAIT_END)
        {
            delayEnd();
        }
    }

    private void delayStart()
    {
        if (waitTimer >= 250)
        {
            waitTimer = 0;
            scrollState = ScrollState.SCROLLING;
        }

        waitTimer++;
    }

    public void Scroll()
    {
        if (waitTimer >= 10)
        {
            waitTimer = 0;
            if (currentScroll < 1.0)
            {
                horizonalScrollBar.value = currentScroll;
                currentScroll += 0.01f;
            }
            else
            {
                scrollState = ScrollState.WAIT_END;
            }
        }

        waitTimer++;
    }

    public void delayEnd()
    {
        if (waitTimer >= 250)
        {
            waitTimer = 0;
            horizonalScrollBar.value = 0;
            currentScroll = 0;
            scrollState = ScrollState.WAIT_START;
        }

        waitTimer++;
    }
}