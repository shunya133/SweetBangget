using UnityEngine;

public class EnemySpawnPoint01 : MonoBehaviour
{
    public GameObject enemyPrefab01;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            Instantiate(enemyPrefab01,transform.position,transform.rotation);
              
        }


    }
}
