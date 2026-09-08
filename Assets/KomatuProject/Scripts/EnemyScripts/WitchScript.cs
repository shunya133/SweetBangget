using UnityEngine;
using UnityEngine.UI;
public class WitchScript : MonoBehaviour
{
    public float enemyMaxHP = 10000;
    public float enemyHP = 10000;
    public int enemyScore = 70;
    //public Slider HPBar;
    void Start()
    {

    }

    void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        //this.enemyHP -= damage;
        int damageBonusPoint = Mathf.FloorToInt(damage);
        GManager.instance.AddTeamScore(enemyScore + damageBonusPoint);//（70 + 弾の攻撃力）をボーナスポイントとして加算
    }

}