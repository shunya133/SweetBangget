using UnityEngine;

public class GunController01 : MonoBehaviour
{
    public static GunController01 instance;
    public SerialReceiver01 receiver;
    public Muzzle01 muzzle01;
    public Quaternion orientation;
    public Quaternion Senserori;
    public Quaternion Sensorinitialori;
    public Quaternion Qsensorori;
    private bool setInitialori = false;
    private Quaternion adjustRotation  = Quaternion.Euler(0, 0, -90);
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
        this.receiver = GameObject.Find("SenserReceiver01").GetComponent<SerialReceiver01>();
        this.setInitialori = GManager.instance.Setinitialori01;
        this.Sensorinitialori = receiver.oriVec;
        this.Sensorinitialori = GManager.instance.Initialori01;
    }
    void Update()
    {
        if (receiver.isvValidSensor == true)
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
            Qsensorori =   Quaternion.Inverse(Sensorinitialori) * Qsensorori;

            //Eulerをquaternionに変換
           this.orientation = new Quaternion(Qsensorori.z, Qsensorori.y, Qsensorori.x, Qsensorori.w);
            //初期値+初期値空の変動量
            this.transform.rotation = Quaternion.identity *  orientation;
        }
    }

    public void Doshot() { 
        muzzle01.Shot();
    }
    public void InEmpty() {
        if (GManager.instance.Player01GunType == "GunType02") {
            muzzle01.EmptyShot();
        }
    }
}
