using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.MainMenu.Canvases
{
    public class CanvasControllerButton : Button
    {
        public Canvas ControlledCanvas;
        public bool isDefaultCanvas = false;
        public CanvasController CanvasController;

        public void Start()
        {
            CanvasController.RegisterCanvas(ControlledCanvas, isDefaultCanvas);
            this.onClick.AddListener(SelectCanvas);
        }

        private void SelectCanvas()
        {
            CanvasController.ActivateCanvas(ControlledCanvas);
        }
    }

    [CustomEditor(typeof(CanvasControllerButton))]
    public class CanvasControllerButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            CanvasControllerButton targetButton = (CanvasControllerButton)target;
            targetButton.ControlledCanvas = (Canvas)EditorGUILayout.ObjectField("Controlled Canvas:", targetButton.ControlledCanvas, typeof(Canvas), true);
            targetButton.isDefaultCanvas = EditorGUILayout.Toggle("Is Default", targetButton.isDefaultCanvas);
            targetButton.CanvasController = (CanvasController)EditorGUILayout.ObjectField("Canvas Controller:", targetButton.CanvasController, typeof(CanvasController), true);
            base.OnInspectorGUI();
        }        
    }
}