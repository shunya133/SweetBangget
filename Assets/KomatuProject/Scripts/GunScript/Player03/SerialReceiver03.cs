using UnityEngine;

public class SerialReceiver03 : MonoBehaviour
{
    public static SerialReceiver03 instance;
    public SerialHandler03 serialHandler03;
    public Quaternion oriVec = Quaternion.identity;
    public Muzzle03 Muzzle03;
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
        serialHandler03.OnDataReceived += OnDataReceived;
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
        if (data[0].Substring(0, 7) == "press03") //送られてきた文字が[press03]なら
        {
            GunController03.instance.Doshot();
            if (GManager.instance.CurrentGamestate == GameState.OnReady)
                Muzzle03.Shot();
            if (isValidSensor == false) isValidSensor = true;
        }
        else if (data[0].Substring(0, 7) == "empty03")
        {
            GunController03.instance.InEmpty();
        }
        else if (data[0].Substring(0, 6) == "ori03=")
        {
            //Debug.Log(data[0]);
            data[0] = data[0].Replace("ori03=", "");
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
    public void ChangeMuzzle03()
    {
        this.Muzzle03 = GameObject.Find("Player03").GetComponent<Muzzle03>();
    }
}
