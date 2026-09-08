using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public enum GameState { 
    OnTItle,
    OnReady,
    OnGame,
    OnStory,
    OnResult,
    OnConnect
}
public class GManager : MonoBehaviour
{
    private GameObject timerScript;
    private bool ConnectBluetooth = false;
    private bool CanPresskey = true;
    private bool stateStopper = false;
    public static GManager instance;
    
    public GameState CurrentGamestate;
    public int stageProgress = 1;
    private int HowtoPlayProgress = 0;
    private int TeamScore;
    private GameObject HowtoPlayText;
    public BluetoothChecker bluetoothChecker;
    public UIManager Uimanager;
    public GameObject Player01;
    public GameObject Player02;
    public GameObject Player03;
    public bool Setinitialori01 = false;
    public bool Setinitialori02 = false;
    public bool Setinitialori03 = false;
    public Quaternion Initialori01;
    public Quaternion Initialori02;
    public Quaternion Initialori03;
    public string Player01GunType = "GunType01";
    public string Player02GunType = "GunType01";
    public string Player03GunType = "GunType01";

    public TitleCameramanager titleCameraManager;
    private void Awake()
    {
        //GameManager.Instanceでこのスクリプトを参照できるようになる
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
        this.Initialori01 = Quaternion.identity;
        this.Initialori02 = Quaternion.identity;
        this.Initialori03 = Quaternion.identity;
    }

    void Start()
    {
        Player01GunType = "GunType01";
        Player02GunType = "GunType01";
        Player03GunType = "GunType01";
        this.timerScript = GameObject.Find("TimerManager");
        if (timerScript != null)
            timerScript.GetComponent<CountDown>().time = 40;
        SetGameState(GameState.OnTItle);
        if (GameObject.Find("HowToPlayText")) {
            HowtoPlayText = GameObject.Find("HowToPlayText");
        }
    }

    void Update()
    {
        //Bキーのセンサー接続処理
        if (Input.GetKeyDown(KeyCode.B) && CurrentGamestate != GameState.OnConnect) {
            bluetoothChecker.gameStateNow = CurrentGamestate;
            SetGameState (GameState.OnConnect);
            
        
        }
        if (Input.GetKeyDown(KeyCode.N) && CurrentGamestate == GameState.OnTItle)
        {
            //タイトル画面の初回センサー接続処理
            if (ConnectBluetooth && CanPresskey)
            {
                if (HowtoPlayProgress <= 3)
                {
                    Uimanager.ChangeTitleText(" ");
                    Uimanager.ChangeHowToPlay(HowtoPlayProgress);
                    HowtoPlayProgress += 1;
                }
                else
                {
                    Uimanager.ChangeHowToPlay(HowtoPlayProgress);
                    Uimanager.ChangeTitleText("Nキーでゲームへ");
                    if (HowtoPlayText != null) HowtoPlayText.GetComponent<TextMeshProUGUI>().enabled = true;
                    MoveTitleCamera();
                    SetGameState(GameState.OnReady);

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N) && CurrentGamestate == GameState.OnReady && stateStopper == false)
        {
                Uimanager.PlayFadeout();
                Invoke(nameof(LoadGameScene), 2.0f);
                SetGameState(GameState.OnGame);
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("GameScene");
            SetGameState(GameState.OnGame);
        }
    }
    public void SetGameState(GameState state) { 
        CurrentGamestate = state;
        OnChangedState(state);
    }

    private void OnChangedState(GameState state) {
        switch (state) { 
            case GameState.OnTItle:

                break; 
            case GameState.OnReady:
                stateStopper = true;
                Invoke(nameof(ChangeStateStopper), 2.0f);
                break;
            case GameState.OnGame:

                break;
            case GameState.OnStory:

                break;
            case GameState.OnConnect:
                bluetoothChecker.StartSensorConnect();
                
                Time.timeScale = 0;
                break;
        
        }
    }
    private void ChangeStateStopper()
    {
        stateStopper = false;
    }
    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void AddTeamScore(int score) {
        GetComponent<AudioSource>().Play();
        this.TeamScore += score;
        OnChangedTeamScore(TeamScore);
    }
    private void OnChangedTeamScore(int score) {
        Uimanager.ChangeScoreText(score); //UIManagerのテキスト変更関数を呼ぶ
        Debug.Log(TeamScore);
    }
    public void MoveTitleCamera() {
        if (CurrentGamestate == GameState.OnTItle && titleCameraManager != null)
            titleCameraManager.AnimateCameraAnim();
    }
    public void ChangeCanPresskey() { 
        CanPresskey = true;
        ConnectBluetooth = true;
    }

    public void SetSensorInitialori(int playernum, Quaternion SensorInitialOri) { 
        if(playernum == 1) 
            Initialori01 = SensorInitialOri;
            Setinitialori01 = true;
        if(playernum == 2) 
            Initialori02 = SensorInitialOri;
            Setinitialori02 = true;
        if(playernum == 3)
            Initialori03 = SensorInitialOri;
            Setinitialori03 = true;    
    }
    public void ActivePlayersComp() {
        Player01.SetActive(true);
        Player02.SetActive(true);
        Player03.SetActive(true);
        ComponentsEnable(Player01);
        ComponentsEnable(Player02);
        ComponentsEnable(Player03);
    }
    private void ComponentsEnable(GameObject Player) {
        Debug.Log("Comp");
        MonoBehaviour[] components = Player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour comp in components)
        {
            comp.enabled = true;
        }
    }
    //キャラクターごとのパラメーターを設定
    public void SetParameter() {
        Player01.GetComponent<Muzzle01>().SetBulletParam(Player01GunType);
        Player02.GetComponent<Muzzle02>().SetBulletParam(Player02GunType);
        Player03.GetComponent<Muzzle03>().SetBulletParam(Player03GunType);
    }

    public void OpenResult() {
        Uimanager.PlayFadeIn();
        SetGameState(GameState.OnTItle);
    }
    public int ReturnTotalScore() { 
        return TeamScore;
    }
    public void ResetVariable() {
        ConnectBluetooth = false;
        CanPresskey = true;
        stateStopper = false;
        SetGameState(GameState.OnTItle);
        stageProgress = 1;
        HowtoPlayProgress = 0;
        TeamScore = 0;
    }
}
