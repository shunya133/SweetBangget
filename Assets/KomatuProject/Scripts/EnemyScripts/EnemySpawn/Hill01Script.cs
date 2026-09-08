using UnityEngine;

public class Hill01Script : MonoBehaviour
{
    public GameObject Hill01Prefab;
    public Transform Hill01SpawnPoint01;
    public Transform Hill01SpawnPoint02;
    public Transform Hill01SpawnPoint03;
    public Transform Hill01SpawnPoint04;
    public Transform[] Hill01SpawnPointR;
    public Transform[] Hill01SpawnPointC;
    public Transform[] Hill01SpawnPointL;
    public GameObject enemyPrefab;
    public GameObject[] EnemyType_Right;
    public GameObject[] enemyType_Centor;
    public GameObject[] EnemyType_Left;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            SpawnCenter(0,4);
        }
    }

    public void SpawnCenter(int Type, int Enemynum)
    {
        for (int i = 0; i < Enemynum; i++)
        {
            GameObject newEnemy = Instantiate(enemyType_Centor[Type], Hill01SpawnPointC[i].position, Hill01SpawnPointC[i].rotation);
            Destroy(newEnemy, 10.0f);
        }
    }
    public void SpawnRight(int Type, int Enemynum) {
        for (int j = 0; j < Enemynum; j++) {
            GameObject newEnemy = Instantiate(EnemyType_Right[Type], Hill01SpawnPointR[j].position, Hill01SpawnPointR[j].rotation);
            Destroy(newEnemy,10.0f);
        }
    
    }
    public void SpawnLeft(int Type, int EnemyNum) {
        for(int k = 0; k < EnemyNum; k++){
            GameObject newEnemy = Instantiate(EnemyType_Left[Type], Hill01SpawnPointL[k].position, Hill01SpawnPointL[k].rotation);
            Destroy(newEnemy, 10.0f);
        }
    }

}
