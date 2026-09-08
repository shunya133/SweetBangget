using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameTimerController : MonoBehaviour
{
    public float time = 40f;
    private float recordTime;
    public TimerAnimation timerAnimation;
    public TMP_Text timerText;
    public TMP_Text timeUPText;
    public Transform WitchSpawnPoint;
    public GameObject Enemy_Witch;
    private Hill01Script hill01Script;
    private Hill02Script hill02Script;
    private Hill03Script hill03Script;

    public bool b_Spawn40 = false;
    [SerializeField]
    private bool b_Spawn30 = false;
    private bool b_Spawn20 = false;
    private bool b_Spawn10 = false;
    public bool ActiveTimer = true;
    public bool b_Spawn0 = false;
    public CSVScript csvScript;
    void Start()
    {
        this.hill01Script = GameObject.Find("Hill01SpawnSystem").GetComponent<Hill01Script>();
        this.hill02Script = GameObject.Find("Hill02SpawnSystem").GetComponent<Hill02Script>();
        this.hill03Script = GameObject.Find("Hill03SpawnSystem").GetComponent<Hill03Script>();
        recordTime = time;

    }
    void Update()
    {
        if (ActiveTimer)
        {
            if (time > 0)
            {
                
                time -= Time.deltaTime;
                if (recordTime - time >= 1f)
                {
                    timerAnimation.PlayAnimation();
                    recordTime = time;
                }
                

                if (time < 0)
                {
                    time = 0;
                }

                timerText.text = time.ToString("F0");
            }
            else
            {
                if (time <= 0 && b_Spawn0 == false)
                {
                    b_Spawn0 = true;
                    //GManager.instance.ChangeStory();
                    GManager.instance.SetGameState(GameState.OnStory);
                    if (GManager.instance.stageProgress == 1) StartCoroutine(ChangeStory(GManager.instance.stageProgress - 1));
                    if (GManager.instance.stageProgress == 2) StartCoroutine(ChangeStory(GManager.instance.stageProgress - 1));
                    if (GManager.instance.stageProgress == 3) StartCoroutine(ChangeStory(GManager.instance.stageProgress - 1));
                    timeUPText.text = "TIME UP";

                }
            }
            //1マップ目のスポーン処理
            if (GManager.instance.CurrentGamestate == GameState.OnGame && GManager.instance.stageProgress == 1)
            {
                if (time <= 39 && b_Spawn40 == false)
                {
                    b_Spawn40 = true;
                    Debug.Log("Spawn40");
                    hill01Script.SpawnLeft(1, 3);
                    hill02Script.SpawnLeft(2, 4);
                }
                if (time <= 30 && b_Spawn30 == false)
                {
                    b_Spawn30 = true;
                    hill01Script.SpawnCenter(0, 4);
                    hill03Script.SpawnRight(2, 4);
                }
                if (time <= 20 && b_Spawn20 == false)
                {
                    b_Spawn20 = true;
                    hill01Script.SpawnLeft(1, 4);
                    hill02Script.SpawnRight(1, 4);
                }
                if (time <= 10 && b_Spawn10 == false)
                {
                    b_Spawn10 = true;
                    hill01Script.SpawnLeft(0, 4);
                    hill02Script.SpawnCenter(2,3);
                    hill03Script.SpawnRight(0,4);
                }
            }
            //2ステージ目のスポーン処理
            if (GManager.instance.CurrentGamestate == GameState.OnGame && GManager.instance.stageProgress == 2)
            {
                if (time <= 39 && b_Spawn40 == false)
                {
                    b_Spawn40 = true;
                    hill01Script.SpawnLeft(1, 3);
                    hill02Script.SpawnLeft(2, 3);
                    hill03Script.SpawnLeft(3, 3);
                }
                if (time <= 30 && b_Spawn30 == false)
                {
                    b_Spawn30 = true;
                    hill01Script.SpawnRight(3, 3);
                    hill02Script.SpawnLeft(4, 3);
                    hill03Script.SpawnRight(1,3);
                    
                }
                if (time <= 20 && b_Spawn20 == false)
                {
                    b_Spawn20 = true;
                    hill01Script.SpawnLeft(4, 4);
                    hill02Script.SpawnRight(2, 4);
                }
                if (time <= 10 && b_Spawn10 == false)
                {
                    b_Spawn10 = true;
                    hill01Script.SpawnCenter(0, 4);
                    hill02Script.SpawnRight(3, 3);
                    hill03Script.SpawnCenter(0, 4);
                }

            }
            if (GManager.instance.CurrentGamestate == GameState.OnGame && GManager.instance.stageProgress == 3) {
                if (time <= 39 && b_Spawn40 == false)
                {
                    b_Spawn40 = true;
                    Instantiate(Enemy_Witch, WitchSpawnPoint);
                }
            
            }
        }
    }
    public void ResetSpawnBool() { 
    
    }
    public IEnumerator ChangeStory(int i ) {
            yield return StartCoroutine(csvScript.StoryPlay(i));
        GManager.instance.SetGameState(GameState.OnGame);
        GManager.instance.stageProgress += 1;
        ResetTimerBool();
        if (i == 2) { 
            GManager.instance.OpenResult();
            UIManager.instance.PlayFadeIn();
            SceneManager.LoadScene("ResultScene");
            //リザルト画面表示

        }
        yield return null;
    }
    private void ResetTimerBool() { 
        b_Spawn40 = false;
        b_Spawn30 = false;
        b_Spawn20 = false;
        b_Spawn10=false;
        b_Spawn0 = false;
        time = 40;
        recordTime = 40;
    }
}
