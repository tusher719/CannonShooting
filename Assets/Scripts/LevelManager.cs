using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    [Header("Level Settings")]
    [SerializeField] LevelData[] levelDataArray;
    [SerializeField] int currentLevelIndex = 0;

    [Header("Score Popup")]
    [SerializeField] GameObject scorePopupPrefab;

    private GameObject currentLevel;
    private int remainingBullets;
    private int totalBoxes;
    private int destroyedBoxes;
    private int score;
    private bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int index)
    {
        if (index >= levelDataArray.Length)
        {
            return;
        }

        if (currentLevel != null)
            Destroy(currentLevel);

        LevelData levelData = levelDataArray[index];
        currentLevel = Instantiate(levelData.levelPrefab, Vector3.zero, Quaternion.identity);

        remainingBullets = levelData.bulletLimit;
        totalBoxes = currentLevel.GetComponentsInChildren<TargetObject>().Length;
        destroyedBoxes = 0;
        score = 0;
        isGameOver = false;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        UpdateUI();
    }

    public void TargetDestroyed()
    {
        destroyedBoxes++;
        UpdateUI();

        if (destroyedBoxes >= totalBoxes && !isGameOver)
        {
            WinGame();
        }
    }

    public void BulletUsed()
    {
        remainingBullets--;
        UpdateUI();

        if (remainingBullets <= 0 && destroyedBoxes < totalBoxes && !isGameOver)
        {
            Invoke("CheckLoseCondition", 3f);
        }
    }

    void CheckLoseCondition()
    {
        if (destroyedBoxes < totalBoxes && !isGameOver)
        {
            LoseGame();
        }
    }

    public void AddScore(int amount, Vector3 worldPos)
    {
        score += amount;
        UpdateUI();
        ShowPopupText("+" + amount, worldPos);
    }

    void ShowPopupText(string text, Vector3 position)
    {
        if (scorePopupPrefab != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, position, Quaternion.identity);
            ScorePopup popupScript = popup.GetComponent<ScorePopup>();
            if (popupScript != null)
            {
                popupScript.ShowPopup(text);
            }
        }
    }

    void UpdateUI()
    {
        if (bulletText != null)
        {
            bulletText.text = $"{remainingBullets}";
        }

        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }

        if (targetText != null)
        {
            int remainingTargets = totalBoxes - destroyedBoxes;
            targetText.text = $"{remainingTargets}";
        }
    }

    void WinGame()
    {
        isGameOver = true;
        if (winPanel != null)
            winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void LoseGame()
    {
        isGameOver = true;
        if (losePanel != null)
            losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public bool CanShoot()
    {
        return !isGameOver && remainingBullets > 0;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        LoadLevel(currentLevelIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        currentLevelIndex++;
        if (currentLevelIndex < levelDataArray.Length)
        {
            LoadLevel(currentLevelIndex);
        }
    }
}