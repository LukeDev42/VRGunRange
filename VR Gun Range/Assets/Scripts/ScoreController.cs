using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public Text bulletCountText;

    private int score;
    private int bulletCount;
    private Shoot shootScript;

    private void Start()
    {
        score = 0;
        AddScore(0);
        bulletCount = 0;
        AddToBulletCount(0);
    }

    public void AddScore(int additionalScore)
    {
        score += additionalScore;
        scoreText.text = "Score: " + score;
    }

    public void AddToBulletCount(int additionalBullet)
    {
        bulletCount += additionalBullet;
        bulletCountText.text = "Bullet Count: " + bulletCount;
    }
}
