using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private List<Canvas> CanvasesToControl = new List<Canvas>();
    private Canvas SelectedCanvas = null;

    public void RegisterCanvas(Canvas canvas, bool isSelected)
    {
        if (!CanvasesToControl.Contains(canvas))
        {
            CanvasesToControl.Add(canvas);
        }

        if (isSelected)
        {
            SelectedCanvas = canvas;
        }

        UpdateCanvases();
    }

    public void ActivateCanvas(Canvas canvas)
    {
        if (CanvasesToControl.Contains(canvas))
        {
            SelectedCanvas = canvas;
        }
        
        UpdateCanvases();
    }

    private void UpdateCanvases()
    {
        CanvasesToControl.ForEach(canvas =>
        {
            if (canvas == SelectedCanvas)
            {
                canvas.gameObject.SetActive(true);
            }
            else
            {
                canvas.gameObject.SetActive(false);
            }
        });
    }
}
