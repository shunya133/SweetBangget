using UnityEngine;

public class SerialReceiver02 : MonoBehaviour
{
    public static SerialReceiver02 instance;
    public SerialHandler02 serialHandler02;
    public Quaternion oriVec = Quaternion.identity;
    public Muzzle02 Muzzle02;
    public bool isValidSensor = true;

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
        serialHandler02.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string message)
    {
        Vector3 pos = transform.localPosition;
        var data = message.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        try
        {
            //Debug.Log(data[0]);//Unityのコンソールに受信データを表示
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
        if (data[0].Substring(0, 7) == "press02") //送られてきた文字が[press02]なら
        {
            GunController02.instance.Doshot();
            if (GManager.instance.CurrentGamestate == GameState.OnReady)
               Muzzle02.Shot();
            if (isValidSensor == false) isValidSensor = true;
        }
        else if (data[0].Substring(0, 7) == "empty02") {
            GunController02.instance.InEmpty();
        }
        else if (data[0].Substring(0, 6) == "ori02=")
        {
            //Debug.Log(data[0]);
            data[0] = data[0].Replace("ori02=", "");
            string[] orivec = data[0].Split(",");

            this.oriVec = new Quaternion(float.Parse(orivec[0]), float.Parse(orivec[1]), float.Parse(orivec[2]), float.Parse(orivec[3]));
            if (isValidSensor == false) isValidSensor = true;
        }
        
    }
    void Update()
    {
        if (this.oriVec == Quaternion.identity)
        {
            isValidSensor = false;
        }

    }
    public void ChangeMuzzle02() {
        this.Muzzle02 = GameObject.Find("Player02").GetComponent<Muzzle02>();
    }
}
