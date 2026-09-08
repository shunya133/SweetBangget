using Unity.Mathematics;
using UnityEngine;

public class Cube_orientation_controller : MonoBehaviour
{
    public GameObject senser;
    public Vector3 orientation;
    private Vector3 initialpos;
    private Transform Mytramsform;
    public Vector3 Senserori;
    public Vector3 initialori ;
    void Start()
    {
        this.senser = GameObject.Find("SenserReceiver");
        initialpos = this.transform.eulerAngles;
        Mytramsform = this.transform;
        this.initialori = senser.GetComponent<SerialReceive>().oriVec;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ZeroVec = new Vector3(0,0,0);
        if (this.initialori == ZeroVec) { 
            this.initialori = senser.GetComponent<SerialReceive>().oriVec;
        }
        this.Senserori = senser.GetComponent<SerialReceive>().oriVec + initialori;
        this.orientation = new Vector3(-Senserori.y, Senserori.x, -Senserori.z);
        Mytramsform.eulerAngles = initialpos + orientation;
    }
}
