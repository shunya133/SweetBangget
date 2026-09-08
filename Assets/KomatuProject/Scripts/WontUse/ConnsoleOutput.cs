using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnsoleOutput : MonoBehaviour
{
    public SerialHandler serialhandler;
    void Start()
    {
        serialhandler.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string message) { 
        var data = message.Split(new string[] { "/n"},System.StringSplitOptions.None);
        if (data.Length != 1) return;
        Debug.Log(data[0]);
    }
    void Update()
    {
        
    }
}
