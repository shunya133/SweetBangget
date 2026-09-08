using UnityEngine;

public class TitleGunController03 : MonoBehaviour
{
    public SerialReceiver03 receiver;
    public Quaternion orientation;
    public Quaternion Senserori;
    public Quaternion Sensorinitialori;
    public Quaternion Qsensorori;
    private bool setInitialori = false;
    void Start()
    {
        // this.receiver = GameObject.Find("SenserReceiver01").GetComponent<SerialReceiver01>();
        //GameManagerから保存している値の受け取り
        this.setInitialori = GManager.instance.Setinitialori03;
        this.Sensorinitialori = receiver.oriVec;
        this.Sensorinitialori = GManager.instance.Initialori03;
    }

    void Update()
    {
        if (this.receiver.isValidSensor == true)
        {
            this.Senserori = receiver.oriVec;
            Quaternion ZeroVec = new Quaternion(0, 0, 0, 1);
            if (this.Sensorinitialori == ZeroVec && setInitialori == false)
            {
                //最初に１度だけ、initialoriの値をセンサーで送られてきた値に変更
                this.Sensorinitialori = Senserori;


                GManager.instance.SetSensorInitialori(3, Sensorinitialori);
                Debug.Log(Senserori);
                if(Senserori != Quaternion.identity)
                setInitialori = true;
            }

            Qsensorori = Senserori;
            Qsensorori = Quaternion.Inverse(Sensorinitialori) * Qsensorori;
            //TitleGunControllerではyとzに代入する値を入れ替え
            this.orientation = new Quaternion(Qsensorori.x, Qsensorori.y, Qsensorori.z,-Qsensorori.w);
            //初期値+初期値空の変動量
            this.transform.rotation = Quaternion.identity * orientation;
        }
    }
}
