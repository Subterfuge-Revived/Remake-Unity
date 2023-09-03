using UnityEngine;
using UnityEngine.UI;

public class SpriteRotator : MonoBehaviour
{

    public Image ImageToRotate;
    public float rotationVelocity = 0.1f;

    private Vector3 pivot;

    void Start()
    {
        pivot = ImageToRotate.rectTransform.TransformPoint(ImageToRotate.rectTransform.pivot);
    }

    // Update is called once per frame
    void Update()
    {
        ImageToRotate.rectTransform.RotateAround(pivot, Vector3.forward, rotationVelocity);
    }
}
