using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class UIPointerController : MonoBehaviour
{
    public GameObject Pointer;
    public GameObject Wall;
    public GameObject StartObject;
    private Vector3 Pointerpos;
    public GameObject CursorManager;
    private RectTransform recttransform;
    private float scale = 75.0f;

    void Start()
    {
        this.CursorManager = GameObject.Find("CursorManager");
        recttransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var n = Wall.transform.forward;
        var x1 = Wall.transform.position;
        var x0 = StartObject.transform.position;
        var f = StartObject.transform.forward;
        var h  = Vector3.Dot(n, x1);
        var intersectPoint = x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, f))) * f;


        //Pointer.transform.position = intersectPoint + new Vector3(0, 0, -10);
        if (CursorManager.GetComponent<CursorController01>() != null)
        {
            this.Pointerpos = CursorManager.GetComponent<CursorController01>().Rayhitpos;
        }
        if (CursorManager.GetComponent<TitleCursorController01>() != null)
        {
            this.Pointerpos = CursorManager.GetComponent<TitleCursorController01>().Rayhitpos;
        }
        float x = intersectPoint.x * scale;
        float y = intersectPoint.y * scale;
        recttransform.anchoredPosition = new Vector3(x,y,0);
    }
}
