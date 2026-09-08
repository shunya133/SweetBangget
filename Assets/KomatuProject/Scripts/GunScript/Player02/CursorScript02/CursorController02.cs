using UnityEngine;

public class CursorController02 : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 direction;
    public GameObject Gun;
    public Vector3 Rayhitpos;
    void Start()
    {
        this.Gun = GameObject.Find("Player02");

    }

    void Update()
    {
        if (this.Gun == null) {
            if(GameObject.Find("Player02"))
            Gun = GameObject.Find("Player02");
        }
        if (Gun != null)
        {
            this.origin = Gun.transform.position;
            this.direction = Gun.transform.forward;
        }
        //Debug.DrawRay(origin, direction,Color.red);
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 50.0f))
        {
            this.Rayhitpos = hit.point;
            Debug.Log(Rayhitpos);
        }
    }
}
