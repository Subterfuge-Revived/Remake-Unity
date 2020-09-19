using UnityEngine;
using System;
using System.Collections.Generic;
using SubterfugeCore.Core.Topologies;

public class WorldWrap : MonoBehaviour
{
    public Transform LeftEndPoint;
    public Transform RightEndPoint;
    public float WorldWidth = -1;
    public float ScreenSize = -1;
    public Camera secCam;
    public Camera mainCam;
    
    /// <summary>
    /// 1 arg: returns 3x3 lattice centered at origin. 5/9 args: returns sufficient lattice to cover viewport.
    /// </summary>
    public List<Vector2> RenderPos(RftVector v)
    {
	    var positions = new List<Vector2>();
	    for (int i = 1; i >= -1; i--)
	    {
		    for (int j = -1; j <= 1; j++)
		    {
			    positions.Add(new Vector2(j * RftVector.Map.Width + v.X, i * RftVector.Map.Height + v.Y));
		    }
	    }
	    return positions;
    }

    public List<Vector2> RenderPos(RftVector v, float objLeft, float objRight, float objTop, float objBottom, float objWidth)
    {
	    float viewportLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
	    float viewportRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0)).x;
	    float viewportTop = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0)).y;
	    float viewportBottom = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
	    var positions = new List<Vector2>();
	    for (float i = v.Y + (float)Math.Floor((viewportTop+objBottom-v.Y)/RftVector.Map.Height) * RftVector.Map.Height;
		    i > viewportBottom - objTop; i -= RftVector.Map.Height)
	    {
		    for (float j = v.X + (float)Math.Floor((viewportLeft+objRight-v.X)/RftVector.Map.Width) * RftVector.Map.Width;
			    j < viewportRight + objLeft; j += RftVector.Map.Width)
		    {
			    positions.Add(new Vector2(j, i));
		    }
	    }
	    return positions;
    }
    
    public List<Vector2> RenderPos(RftVector v, float objLeft, float objRight, float objTop, float objBottom, float objWidth, 
	    float viewportLeft, float viewportRight, float viewportTop, float viewportBottom)
    {
	    var positions = new List<Vector2>();
	    for (float i = v.Y + (float)Math.Floor((viewportTop+objBottom-v.Y)/RftVector.Map.Height) * RftVector.Map.Height;
		    i > viewportBottom - objTop; i -= RftVector.Map.Height)
	    {
		    for (float j = v.X + (float)Math.Floor((viewportLeft+objRight-v.X)/RftVector.Map.Width) * RftVector.Map.Width;
			    j < viewportRight + objLeft; j += RftVector.Map.Width)
		    {
			    positions.Add(new Vector2(j, i));
		    }
	    }
	    return positions;
    }
    
    void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;
        if (secCam == null)
        {
            secCam = GetComponent<Camera>();
            secCam.transform.parent = mainCam.transform;
            secCam.clearFlags = CameraClearFlags.Nothing;
        }
        if (WorldWidth < 0)
            WorldWidth = RightEndPoint.position.x - LeftEndPoint.position.x;
        if (ScreenSize < 0)
        {
            var p = mainCam.ViewportToWorldPoint(new Vector3(0, 0.5f, -mainCam.transform.position.z));
            ScreenSize = (mainCam.transform.position - p).x * 2;
        }
    }
    
    void LateUpdate()
    {
        var d = mainCam.transform.position.x - LeftEndPoint.position.x;
        if (d < ScreenSize)
        {
            mainCam.transform.position = -mainCam.transform.position;
        }
    }
}