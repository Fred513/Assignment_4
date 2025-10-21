using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public int points = 50; // 能量豆分数更高

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            GameManager.Instance.FrightenGhosts(); // 让鬼魂进入害怕状态（新功能）
            Destroy(gameObject);
        }
    }
}
