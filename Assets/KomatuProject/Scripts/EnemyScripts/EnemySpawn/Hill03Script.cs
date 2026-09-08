using UnityEngine;

public class Hill03Script : MonoBehaviour
{
    public Transform[] Hill03SpawnPointR;
    public Transform[] Hill03SpawnPointC;
    public Transform[] Hill03SpawnPointL;
    public GameObject[] EnemyType_Right;
    public GameObject[] enemyType_Centor;
    public GameObject[] EnemyType_Left;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnCenter(0, 4);
        }
    }

    public void SpawnCenter(int Type, int Enemynum)
    {
        for (int i = 0; i < Enemynum; i++)
        {
            GameObject newEnemy = Instantiate(enemyType_Centor[Type], Hill03SpawnPointC[i].position, Hill03SpawnPointC[i].rotation);
            Destroy(newEnemy, 10.0f);
        }
    }
    public void SpawnRight(int Type, int Enemynum)
    {
        for (int j = 0; j < Enemynum; j++)
        {
            GameObject newEnemy = Instantiate(EnemyType_Right[Type], Hill03SpawnPointR[j].position, Hill03SpawnPointR[j].rotation);
            Destroy(newEnemy, 10.0f);
        }

    }
    public void SpawnLeft(int Type, int EnemyNum)
    {
        for (int k = 0; k < EnemyNum; k++)
        {
            GameObject newEnemy = Instantiate(EnemyType_Left[Type], Hill03SpawnPointL[k].position, Hill03SpawnPointL[k].rotation);
            Destroy(newEnemy, 10.0f);
        }
    }

}
