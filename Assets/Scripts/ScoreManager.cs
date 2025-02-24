using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���������� TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int playerScore = 0;
    public int botScore = 0;

    // ���������� TMP_Text ��� TextMeshPro ������ Text
    public TMP_Text playerScoreText;
    public TMP_Text botScoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPlayerScore(int points)
    {
        playerScore += points;
        UpdateUI();
    }

    public void AddBotScore(int points)
    {
        botScore += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerScoreText != null)
            playerScoreText.text = "�����: " + playerScore;

        if (botScoreText != null)
            botScoreText.text = "����: " + botScore;
    }
}
