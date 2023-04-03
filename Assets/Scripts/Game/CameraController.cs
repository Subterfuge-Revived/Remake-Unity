﻿using Subterfuge.Remake.Core.Topologies;
using UnityEngine;
using UnityEngine.EventSystems;

 public class CameraController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float cameraPanDampen = 5;
    public float cameraZoomDampen = 5;
    public float cameraZoomScrollSpeed = 0.33f; // Make this configurable in User Settings?
    public float maxZoomOrthographicSize = 3;
    public float minZoomOrthographicSize = 300;
    public float mapHeight = 400; // This is the map height for the particular game. This only applies to rectangular maps.
    public float mapWidth = 200; // This is the map width for the particular game. This only applies to rectangular maps.
    private Vector3 dragOrigin;
    private float speed;
    private RaycastHit2D hit;
    private GameManager gameManager;
    
    // Variables for tracking sub launches.
    // private Outpost launchOutpost = null;
    // private Outpost destinationOutpost = null;
    
    // Track object being held/dragged
    private bool draggingMap = false;

    void SmartZoom(Vector3 zoomCenter, float zoomFactor)
    {
        var posBeforeZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Camera.main.orthographicSize /= zoomFactor;
        if (Camera.main.orthographicSize < maxZoomOrthographicSize) Camera.main.orthographicSize = maxZoomOrthographicSize;
        if (Camera.main.orthographicSize > minZoomOrthographicSize) Camera.main.orthographicSize = minZoomOrthographicSize;
        var posAfterZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.Translate(posBeforeZoom - posAfterZoom, Space.World);
    }

    void OnStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the camera has a velocity, dampen the velocity.
        if (rb.velocity.magnitude > 0)
        {
            if (rb.velocity.magnitude < 0.01) rb.velocity = new Vector2(0, 0);
            else rb.velocity *= 1 - (Time.deltaTime * cameraPanDampen);
        }
        
        // If the camera has a zoom velocity, dampen the zoom. (Not yet implemented)

        // If the pointer is over the map OR if the initial touch/click point was the map or outposts, continue.
        if ((!EventSystem.current.IsPointerOverGameObject()) || (draggingMap))
        {
	        // When the left mouse button is clicked, create a dragOrigin and velocity for the camera.
	        if (Input.GetMouseButtonDown(0))
	        {
		        // Reset all already-existing map movement.
		        rb.velocity = new Vector2(0, 0);
		        // Firstly, check that the pressed location wasn't an outpost.
		        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		        if (hit.collider == null || !hit.collider.gameObject.CompareTag("Outpost"))
		        {
			        // The pressed location was not an outpost. Create a dragOrigin and velocity for the camera.
			        dragOrigin = Input.mousePosition;
			        draggingMap = true;
			        return;
		        }
	        }
        
	        // If the left mouse button is being held AND the initial touch/click point was on the map, pan the camera.
	        if ((Input.GetMouseButton(0)) && (draggingMap))
	        {
		        if (dragOrigin == Input.mousePosition) return;
		        Vector3 pos = Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		        transform.Translate(pos, Space.World);
		        dragOrigin = Input.mousePosition;
	        }

	        // If the left mouse button is released AND the initial touch/click was on the map, begin pan dampening.
	        if ((Input.GetMouseButtonUp(0)) && (draggingMap))
	        {
		        rb.velocity = ((Camera.main.ScreenToWorldPoint(dragOrigin) - Camera.main.ScreenToWorldPoint(Input.mousePosition)) / Time.deltaTime);
		        draggingMap = false;
	        }
        
	        // If the mouse wheel is being scrolled, zoom the camera.
	        if (Input.mouseScrollDelta.magnitude > 0)
	        {
		        SmartZoom(Input.mousePosition, Mathf.Pow(2, (Input.mouseScrollDelta.y * cameraZoomScrollSpeed)));
	        }
        }

        // Set the camera center modulo map dimensions by wrapping the transform inside an RftVector.
        RftVector cam = new RftVector(RftVector.Map, transform.position.x, transform.position.y);
        transform.SetPositionAndRotation(new Vector3((float)cam.X, (float)cam.Y, transform.position.z), transform.rotation);
    }
}
