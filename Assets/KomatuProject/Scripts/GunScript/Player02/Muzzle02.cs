using UnityEngine;

public class Muzzle02 : MonoBehaviour
{
    public GameObject[] NormalfirePoint;
    public GameObject[] GunType03FirePoint;
    public GameObject Type01Bullet;
    public GameObject Type02Bullet;
    public GameObject Type03Bullet;
    public GameObject Bullet;

    private float BulletSpeed;
    private float BulletDamage;

    public Material PlayerMaterial;
    public Material Type02Material;
    void Start()
    {
        BulletSpeed = 1000.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shot();
        }
    }

    public void Shot()
    {
        for (int i = 0; i < NormalfirePoint.Length; i++)
        {
            GameObject newBullet = Instantiate(Bullet, NormalfirePoint[i].transform.position, Bullet.transform.rotation);
            newBullet.GetComponent<MeshRenderer>().material = PlayerMaterial;
            if (newBullet.GetComponent<BulletController>())
            newBullet.GetComponent<BulletController>().bulletDamage = BulletDamage;
            Rigidbody Bulletrigid = newBullet.GetComponent<Rigidbody>();
            Bulletrigid.AddForce(NormalfirePoint[i].transform.forward * BulletSpeed);
            Destroy(newBullet, 10.0f);
        }
        if (GManager.instance.Player02GunType == "GunType03")
        {
            for (int j = 0; j < GunType03FirePoint.Length; j++)
            {
                GameObject newBullet = Instantiate(Bullet, GunType03FirePoint[j].transform.position, Bullet.transform.rotation);
                newBullet.GetComponent<MeshRenderer>().material = PlayerMaterial;
                if (newBullet.GetComponent<BulletController>())
                    newBullet.GetComponent<BulletController>().bulletDamage = BulletDamage;
                Rigidbody Bulletrigid = newBullet.GetComponent<Rigidbody>();
                Bulletrigid.AddForce(GunType03FirePoint[j].transform.forward * BulletSpeed);
                Destroy(newBullet, 10.0f);
            }
        }
    }
    public void EmptyShot() {
        for (int i = 0; i < NormalfirePoint.Length; i++)
        {
            GameObject newBullet = Instantiate(Type02Bullet, NormalfirePoint[i].transform.position, Bullet.transform.rotation);
            if (newBullet.GetComponent<BulletController>())
                newBullet.GetComponent<BulletController>().bulletDamage = BulletDamage;
            Rigidbody Bulletrigid = newBullet.GetComponent<Rigidbody>();
            Bulletrigid.AddForce(NormalfirePoint[i].transform.forward * BulletSpeed);
            Destroy(newBullet, 10.0f);
        }
    }
    public void SetBulletParam(string Guntype)
    {
        if (GManager.instance.CurrentGamestate != GameState.OnReady)
        {
            switch (Guntype)
            {
                case "GunType01":
                    this.Bullet = Type01Bullet;
                    BulletSpeed = 700;
                    BulletDamage = 50;
                    break;
                case "GunType02":
                    this.Bullet = Type02Bullet;
                    BulletSpeed = 1300;
                    BulletDamage = 35;
                    break;
                case "GunType03":
                    this.Bullet = Type03Bullet;
                    BulletSpeed = 1000;
                    BulletDamage = 20;
                    break;
            }
        }
    }
}
