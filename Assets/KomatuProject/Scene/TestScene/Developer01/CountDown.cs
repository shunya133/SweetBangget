using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public float time = 60f;

    public TMP_Text timerText;
    public TMP_Text timeUPText;

    private float recordTime;

    public TimerAnimation Timeranimation;

    void Start()
    {
        recordTime = time;
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time < 0)
            {
                time = 0;
            }

            if (recordTime - time >= 1f)
            {
                Timeranimation.PlayAnimation();

                recordTime = time;
            }

            timerText.text = ((int)time).ToString();
        }
        else
        {
            timeUPText.text = "TIME UP";
        }
    }
}