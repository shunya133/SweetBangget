using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI TitleText;
    public RawImage HowToPlay01;
    public RawImage HowToPlay02;
    public RawImage HowToPlay03;
    public RawImage HowToPlay04;
    private void Awake()
    {
        //UiIManager.Instanceでこのスクリプトを参照できるようになる
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
            
    }
    void Update()
    {
        if (this.scoreText == null && GameObject.Find("ScoreText")) { 
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
    }
    public void ViewSensorConnectWindow() { 
        GameObject ConnectWindow = GameObject.Find("SensorConnectWindow"); //各シーンに存在するSensorConnectWindowを取得
    }

    public void PlayFadeout() { 
        Animator FadeAnim = GameObject.Find("Fade").GetComponent<Animator>();
        FadeAnim.Play("Anim_Fadeout",0,0);
    }
    public void PlayFadeIn() {
        Animator FadeInAnim = GameObject.Find("Fade").GetComponent<Animator>();
        FadeInAnim.Play("Anim_Fadein", 0,0);
    }
    public void ViewScore() {
        Animator ScoreAnim = GameObject.Find("ScoreWindow").GetComponent<Animator>();
        ScoreAnim.Play("Anim_ScoreWindow", 0, 0);
    }

    public void ChangeScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ViewResultUI(int score) {
        GameObject resultUI =  GameObject.Find("ResultUI");
        TextMeshProUGUI resultSvoreText = resultUI.GetComponent<TextMeshProUGUI>();
        //resultUIのAnimatorを動かす処理
        //textを反映する処理
    }

    public void ChangeTitleText( string message)
    {
      TitleText.text = message;
    }

    public void ChangeHowToPlay(int i) {
        switch (i) {
            case 0:
                HowToPlay01.enabled = true;
                HowToPlay02.enabled = false;
                HowToPlay03.enabled = false;
                HowToPlay04.enabled = false;
                break;
            case 1:
                HowToPlay01.enabled = false;
                HowToPlay02.enabled = true;
                HowToPlay03.enabled = false;
                HowToPlay04.enabled = false;
                break;
            case 2:
                HowToPlay01.enabled = false;
                HowToPlay02.enabled = false;
                HowToPlay03.enabled = true;
                break;
            case 3:
                HowToPlay01.enabled = false;
                HowToPlay02.enabled = false;
                HowToPlay03.enabled = false;
                HowToPlay04.enabled = true;
                break;
            case 4:
                HowToPlay01.enabled = false;
                HowToPlay02.enabled = false;
                HowToPlay03.enabled = false;
                HowToPlay04.enabled = false;
                break;


            default:
                break;

        
        }
    }
}
