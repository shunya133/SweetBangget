using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class CubeController : MonoBehaviour
{
    public Vector3 deltagyrovec;
    public Quaternion deltaQgy;
    public Quaternion Base_Rotation = Quaternion.Euler(90, 0, 0);
    Transform m_transform;
    GameObject senser; //ÉQÅ[ÉÄì‡ÇÃsennserReceiverÇéQè∆
    void Start()
    {
        this.senser = GameObject.Find("SenserReceiver");
        m_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.deltagyrovec = Time.deltaTime * this.senser.GetComponent<SerialReceive>().gyVec;
        this.deltaQgy = quaternion.Euler(deltagyrovec);
       // Debug.Log("Cubecontroller");
        //Debug.Log(deltaQgy);
        m_transform.Rotate(deltagyrovec, Space.Self);

    }
}
