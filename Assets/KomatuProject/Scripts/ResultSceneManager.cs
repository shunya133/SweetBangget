using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public RawImage ResultImage;
    public TextMeshProUGUI ResultScoreText;
    public int TotalScore;
    void Start()
    {
        UIManager.instance.PlayFadeIn();
        ResultScoreText.text = GManager.instance.ReturnTotalScore().ToString();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) { 
            UIManager.instance.PlayFadeout();
            Invoke(nameof(OpenTitleScene), 2.5f);
        }
    }

    private void OpenTitleScene() {
        GManager.instance.ResetVariable();
        SceneManager.LoadScene("TitleScene");
    }
}
