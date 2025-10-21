using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public Text scoreText;        // ������ʾ
    public Text livesText;        // ������ʾ
    public GameObject gameOverPanel; // ��Ϸ�������

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ���·�����ʾ
    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    // ����������ʾ
    public void UpdateLives(int lives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives.ToString();
    }

    // ��ʾ��Ϸ����
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    // ������Ϸ����
    public void HideGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
}
