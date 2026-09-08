using UnityEngine;
using UnityEngine.UI;

public class EnemyScripts01 : MonoBehaviour
{
    public float enemyMaxHP = 100;
    public float enemyHP = 100;
    public int  enemyScore = 100;
    public Slider HPBar;
    void Start()
    {
        this.HPBar.enabled = false;
        HPBar.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    public void TakeDamage(float damage) {
        GetComponent<AudioSource>().Play();
        HPBar.enabled = true;
        HPBar.gameObject.SetActive(true);
        this.enemyHP -= damage;
        HPBar.value = enemyHP;
        
        if (enemyHP <= 0) {
            GManager.instance.AddTeamScore(enemyScore);
            Destroy(transform.parent.gameObject);
        }
    }
    
}
