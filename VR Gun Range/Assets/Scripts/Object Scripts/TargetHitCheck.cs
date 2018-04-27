using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitCheck : MonoBehaviour {

    public ScoreController scoreController;

    private int pointValue;
    
    private void Awake()
    {
        pointValue = 100;
        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        scoreController.AddScore(pointValue);
    }
}
