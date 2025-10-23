using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI totalScoreText;
    int totalScore = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        UpdateScore();
    }

    void UpdateScore()
    {
        if (totalScoreText != null)
            totalScoreText.text = "" + totalScore;
    }
}
