using UnityEngine;

public class Hill02Script : MonoBehaviour
{
    public Transform[] Hill02SpawnPointR;
    public Transform[] Hill02SpawnPointC;
    public Transform[] Hill02SpawnPointL;
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
            GameObject newEnemy = Instantiate(enemyType_Centor[Type], Hill02SpawnPointC[i].position, Hill02SpawnPointC[i].rotation);
            Destroy(newEnemy, 10.0f);
        }
    }
    public void SpawnRight(int Type, int Enemynum)
    {
        for (int j = 0; j < Enemynum; j++)
        {
            GameObject newEnemy = Instantiate(EnemyType_Right[Type], Hill02SpawnPointR[j].position, Hill02SpawnPointR[j].rotation);
            Destroy(newEnemy, 10.0f);
        }

    }
    public void SpawnLeft(int Type, int EnemyNum)
    {
        for (int k = 0; k < EnemyNum; k++)
        {
            GameObject newEnemy = Instantiate(EnemyType_Left[Type], Hill02SpawnPointL[k].position, Hill02SpawnPointL[k].rotation);
            Destroy(newEnemy, 10.0f);
        }
    }

}
