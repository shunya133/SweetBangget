using TMPro;
using UnityEngine;

public class CountDownSample : MonoBehaviour
{
    public float time = 60f;
    public TMP_Text timerText;
    public TMP_Text timeUPText;
    // private float recordTime; ←recordtaimeは他の人から変えられたくないのでprivate
    void Update()
    {
        if (time > 0)
        {
            //recordTime = 60; 最初は60秒
            time -= Time.deltaTime;
            //if(recordtime - time >= 0) →もし前回記録したタイムから1秒以上経過していたら
            //アニメーションを再生する処理
            //recordtime =  time;

            if (time < 0)
            {
                time = 0;
            }

            timerText.text = time.ToString("F0");
        }
        else
        {
            timeUPText.text = "TIME UP";
        }
    }
}