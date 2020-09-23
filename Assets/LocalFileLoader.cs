using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFileLoader : MonoBehaviour
{
    public UniWebView webGui;
    
    // Start is called before the first frame update
    void Start()
    {
        webGui.BackgroundColor = new Color(0,0,0,0);
        string url = UniWebViewHelper.StreamingAssetURLForPath("local_www/index.html");
        webGui.Load(url);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
