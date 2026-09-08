using UnityEngine;

public class CursorController03 : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 direction;
    public GameObject Gun;
    public Vector3 Rayhitpos;
    //private int Layermask = 1 << 8;
    void Start()
    {

        this.Gun = GameObject.Find("Player03");
    }

    void Update()
    {
        this.origin = Gun.transform.position;
        this.direction = Gun.transform.forward;
        //Debug.DrawRay(origin, direction,Color.red);
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 50.0f))
        {
            this.Rayhitpos = hit.point;
            //Debug.Log(Rayhitpos);
        }
    }
}
