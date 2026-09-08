using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
public class Receiveoriserial : MonoBehaviour
{
    public SerialHandler serialHandler;
    public Vector3 oriVec;
    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string message)
    {
        Vector3 pos = transform.localPosition;
        var data = message.Split(
                new string[] { "\n" }, System.StringSplitOptions.None);
        try
        {
            // Debug.Log(data[0]);//Unityのコンソールに受信データを表示
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
        if (data[0].Substring(0, 4) == "ori=")
        {
            //Debug.Log(data[0]);
            data[0] = data[0].Replace("ori=", "");
            string[] orivec = data[0].Split(",");

            this.oriVec = new Vector3(float.Parse(orivec[0]), float.Parse(orivec[1]), float.Parse(orivec[2]));
            //Debug.Log("ori");
            //Debug.Log(oriVec);
        }
    }
    void Update()
    {
        
    }
}
