using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Score scoreScript;
    public CountDown countdownScript;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.B))
        {
            scoreScript.score += 100;
        }

        
        if (Input.GetKeyDown(KeyCode.V))
        {
            countdownScript.time += 10f;
        }
    }
}
