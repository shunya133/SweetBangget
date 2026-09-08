using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletDamage = 35;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")){
            if (other.GetComponent<EnemyScripts01>())
            {
                other.GetComponent<EnemyScripts01>().TakeDamage(bulletDamage);
            }
            else if (other.GetComponent<WitchScript>())
            { 
                other.GetComponent<WitchScript>().TakeDamage(bulletDamage);
            }

                Debug.Log("BulletHit");
            Destroy(gameObject);
        }
    }
}
