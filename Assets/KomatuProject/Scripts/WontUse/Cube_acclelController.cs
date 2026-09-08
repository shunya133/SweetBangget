using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class Cube_acclelCOntroller : MonoBehaviour
{
    public Vector3 lacceleration;
    public Vector3 angularAcceleration;
    public GameObject senser;
    public Vector3 verocity;
    public Vector3 position;
    private Vector3 inisialpos;
    private Vector3 corecctedlac;
    void Start()
    {
        this.senser = GameObject.Find("SenserReceiver");
        inisialpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.lacceleration = this.senser.GetComponent<SerialReceive>().lacVec * 9.8f;
        this.corecctedlac = new Vector3(-lacceleration.y,-lacceleration.x,lacceleration.z);
        this.verocity = this.corecctedlac * Time.deltaTime; //‘¬“x
        if (verocity.magnitude < 0.05f) { 
            verocity = Vector3.zero;
        }
        verocity *= 0.95f;
        this.position = verocity * Time.deltaTime;
        transform.localPosition = transform.localPosition + position * 2;
    }
}
