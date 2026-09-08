using UnityEngine;

public class GunController03 : MonoBehaviour
{
    public static GunController03 instance;
    public SerialReceiver03 receiver;
    public Muzzle03 muzzle03;
    public Quaternion orientation;
    public Quaternion Senserori;
    public Quaternion Sensorinitialori;
    public Quaternion Qsensorori;
    private bool setInitialori = false;
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
    private void OnEnable()
    {
        this.receiver = GameObject.Find("SenserReceiver03").GetComponent<SerialReceiver03>();
        this.setInitialori = GManager.instance.Setinitialori03;
        this.Sensorinitialori = receiver.oriVec;
        this.Sensorinitialori = GManager.instance.Initialori03;
    }

    void Update()
    {
        if (receiver.isValidSensor == true)
        {
            this.Senserori = receiver.oriVec;
            Quaternion ZeroVec = new Quaternion(0, 0, 0, 1);
            if (this.Sensorinitialori == ZeroVec && setInitialori == false)
            {
                //最初に１度だけ、initialoriの値をセンサーで送られてきた値に変更
                this.Sensorinitialori = Senserori;
                GManager.instance.SetSensorInitialori(1, Sensorinitialori);
                setInitialori = true;
            }
            Qsensorori = Senserori;
            //現在の値と、起動時の値の差を確認
            Qsensorori = Quaternion.Inverse(Sensorinitialori) * Qsensorori;

            //Eulerをquaternionに変換
            this.orientation = new Quaternion(Qsensorori.x, Qsensorori.z, Qsensorori.y, -Qsensorori.w);
            //初期値+初期値空の変動量
            this.transform.rotation = Quaternion.identity * orientation;
        }
    }
    public void Doshot()
    {
        muzzle03.Shot();
    }
    public void InEmpty()
    {
        if (GManager.instance.Player03GunType == "GunType02")
        {
            muzzle03.EmptyShot();
        }
    }
}
