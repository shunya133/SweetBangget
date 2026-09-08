using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialReceive : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //上記URLのSerialHandler.cのクラス
    public SerialHandler serialHandler;
    private Vector3 pos;
    public Vector3 acVec;
    public Vector3 gyVec;
    public Vector3 mgVec;
    public Vector3 lacVec;
    public Vector3 oriVec;
    public Vector3 madVec;
    void Start()
    {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //受信した信号(message)に対する処理
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
        if (data[0].Substring(0, 3) == "ac=")
        {
            data[0] = data[0].Replace("ac=", "");
            string[] acvec = data[0].Split(",");

            this.acVec = new Vector3(float.Parse(acvec[0]), float.Parse(acvec[1]), float.Parse(acvec[2]));
            //Debug.Log(acVec);
        }

        if (data[0].Substring(0, 3) == "gy="){

            data[0] = data[0].Replace("gy=", "");
            string[] gyvec = data[0].Split(",");

            this.gyVec = new Vector3(float.Parse(gyvec[0]), float.Parse(gyvec[1]), float.Parse(gyvec[2]));
            this.gyVec = gyVec * Mathf.Rad2Deg;
            //Debug.Log(gyVec);
            //Debug.Log("Can gy");
        }

        if (data[0].Substring(0, 3) == "mg=")
        {
            data[0] = data[0].Replace("mg=", "");
            string[] mgvec = data[0].Split(",");

            this.mgVec = new Vector3(float.Parse(mgvec[0]), float.Parse(mgvec[1]), float.Parse(mgvec[2]));
            //Debug.Log("mg");
            //Debug.Log(mgVec);
        }

        if (data[0].Substring(0, 6) == "linac=")
        {
            //Debug.Log(data[0]);
            data[0] = data[0].Replace("linac=", "");
            string[] lacvec = data[0].Split(",");

            this.lacVec = new Vector3(float.Parse(lacvec[0]), float.Parse(lacvec[1]), float.Parse(lacvec[2]));
            //Debug.Log("lac");
            //Debug.Log(lacVec);
        }

        if (data[0].Substring(0, 4) == "ori=")
        {
            Debug.Log(data[0]);
            data[0] = data[0].Replace("ori=", "");
            string[] orivec = data[0].Split(",");

            this.oriVec = new Vector3(float.Parse(orivec[0]), float.Parse(orivec[1]), float.Parse(orivec[2]));
            //Debug.Log("ori");
            //Debug.Log(oriVec);
        }
        if (data[0].Substring(0, 4) == "Mad=")
        {
            Debug.Log(data[0]);
            data[0] = data[0].Replace("Mad=", "");
            string[] madvec = data[0].Split(",");

            this.madVec = new Vector3(float.Parse(madvec[0]), float.Parse(madvec[1]), float.Parse(madvec[2]));
            //Debug.Log("mad");
            //Debug.Log(madVec);
        }
    }
}

