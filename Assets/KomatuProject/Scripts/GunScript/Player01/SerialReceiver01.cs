using UnityEngine;
//ゲーム内オブジェクト"SenserReceiver01"に設置。Muzzle01項目には、どうオブジェクト内にある"Muzzle01"スクリプトをインスペクターから設定
public class SerialReceiver01 : MonoBehaviour
{
    public Serialhandler01 serialHandler01;
    public static SerialReceiver01 instance;
    public Quaternion oriVec = Quaternion.identity;
    public GunController01 gunController01;
    public Muzzle01 Muzzle01;
    public bool isvValidSensor = true;
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
        serialHandler01.OnDataReceived += OnDataReceived;
        Muzzle01 = GameObject.Find("TitlePlayer01").GetComponent<Muzzle01>();
        
    }

    void OnDataReceived(string message)
    {
        Vector3 pos = transform.localPosition;
        var data = message.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        try
        {
          // Debug.Log(data[0]);//Unityのコンソールに受信データを表示
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
        if (data[0].Substring(0, 7) == "press01") //送られてきた文字が[press01]なら
        {
            GunController01.instance.Doshot();
            if (GManager.instance.CurrentGamestate == GameState.OnReady)
                Muzzle01.Shot();
            if (isvValidSensor == false) isvValidSensor = true;
        }
        else if (data[0].Substring(0, 7) == "empty01") 
        {
            if (gunController01 != null)
            {
                gunController01.InEmpty();
            }
        }
        else if (data[0].Substring(0, 6) == "ori01=")
        {
           // Debug.Log(data[0]);
            data[0] = data[0].Replace("ori01=", "");
            string[] orivec = data[0].Split(",");

            this.oriVec = new Quaternion(float.Parse(orivec[0]), float.Parse(orivec[1]), float.Parse(orivec[2]), float.Parse(orivec[3]));
            if (isvValidSensor == false) isvValidSensor = true;
        }
    }
    void Update()
    {
        if (this.oriVec == Quaternion.identity)
        {
            isvValidSensor = false;
        }
    }
    public void ChangeMuzzle01() {
        this.Muzzle01 = GameObject.Find("Player01").GetComponent<Muzzle01>();
    }
}
