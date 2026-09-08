using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MuzzleController01 : MonoBehaviour
{
    public GameObject Muzzle;
    public GameObject Bullet;
    private float BulletSpeed;
    void Start()
    {
        BulletSpeed = 700.0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z)) {
            Shot();
        }*/
    }

    void Shot() {
        GameObject newBullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        Rigidbody Bulletrigid = newBullet.GetComponent<Rigidbody>();
        Bulletrigid.AddForce(this.Muzzle.transform.forward * BulletSpeed);
        Destroy(newBullet,10.0f);
    }
}
