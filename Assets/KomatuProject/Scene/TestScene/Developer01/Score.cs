using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            score += 100;
            scoreText.text = "Score: " + score;
        }
    }
}