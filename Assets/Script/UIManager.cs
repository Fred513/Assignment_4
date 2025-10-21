using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public Text scoreText;        // 分数显示
    public Text livesText;        // 生命显示
    public GameObject gameOverPanel; // 游戏结束面板

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 更新分数显示
    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    // 更新生命显示
    public void UpdateLives(int lives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives.ToString();
    }

    // 显示游戏结束
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    // 隐藏游戏结束
    public void HideGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
}
