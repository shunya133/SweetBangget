using TMPro;
using UnityEngine;

public class BluetoothChecker : MonoBehaviour
{
    public static BluetoothChecker instance;
    public GameObject sensorReceiver01;
    public GameObject sensorReceiver03;
    public GameObject sensorReceiver02;
    public GameObject Player01;
    public GameObject Player02;
    public GameObject Player03;
    public GameObject SensorConectWindow;
    public TextMeshProUGUI ConnectwindowText;
    private bool onConnect = false;
    public GameState gameStateNow;
    [SerializeField]
    private float ConnectionTimer = 0;
    private bool endConnect = true;

    private void Awake()
    {
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
        if (endConnect == false) ConnectionTimer += 0.1f;
        if (Input.GetKeyDown(KeyCode.B) && onConnect == true) {
            ConnectwindowText.text = "せつぞくちゅう...";
            UIManager.instance.ChangeTitleText("Please Wait...");
                endConnect = false;
            
        }
        if (ConnectionTimer >= 10 && endConnect == false)
        {
            
            sensorReceiver01.SetActive(true);
            sensorReceiver02.SetActive(true);
            sensorReceiver03.SetActive(true);
            Player01.SetActive(true);
            Player02.SetActive(true);
            Player03.SetActive(true);
            ConnectwindowText.text = "せつぞくかんりょう！";
            UIManager.instance.ChangeTitleText("Nキーで次へ");


        }
        if (ConnectionTimer >= 30 && endConnect == false)
        {
            ConnectionTimer = 0;
            Time.timeScale = 1.0f;
            EndSensorConnect();
            endConnect = true;
        }
    }
    public void StartSensorConnect() {
        this.SensorConectWindow.SetActive(true);
        sensorReceiver01.SetActive(false);
        sensorReceiver02.SetActive(false);
        sensorReceiver03.SetActive(false);
        Player01.SetActive(false);
        Player02.SetActive(false);
        Player03.SetActive(false);
        ConnectwindowText.text = "センサーへのせつ続を開始します!じゅうをこちらに向けてください";
        onConnect = true;
    }

    private void EndSensorConnect() {
        Debug.Log("EndConnect");
        onConnect = false ;
        SensorConectWindow.SetActive(false);
        GManager.instance.ChangeCanPresskey();
        GManager.instance.SetGameState(gameStateNow);
    }
}
