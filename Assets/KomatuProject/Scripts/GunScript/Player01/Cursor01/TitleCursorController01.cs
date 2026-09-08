using UnityEngine;

public class TitleCursorController01 : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 direction;
    public GameObject FirePoint;
    public Vector3 Rayhitpos;
    void Start()
    {


    }

    void Update()
    {
        this.origin = FirePoint.transform.position;
        this.direction = FirePoint.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 50.0f))
        {
            this.Rayhitpos = hit.point;
        }
    }
}
