using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;

    // 存放场景中所有鬼魂
    public Ghost[] ghosts;

    // 害怕模式持续时间
    public float frightenedDuration = 5f;

    void Awake() => Instance = this;

    public void AddScore(int amount)
    {
        score += amount;
        UIManager.Instance.UpdateScore(score);
    }

    // ⚡ 让所有鬼魂进入害怕状态
    public void FrightenGhosts()
    {
        foreach (var ghost in ghosts)
        {
            ghost.SetFrightened();
        }

        // 开启计时器，让鬼魂在 frightenedDuration 后恢复
        StartCoroutine(RecoverGhostsAfterTime(frightenedDuration));
    }

    private IEnumerator RecoverGhostsAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var ghost in ghosts)
        {
            if (ghost.state == GhostState.Frightened)
                ghost.state = GhostState.Scatter; // 恢复到散步状态
        }
    }
}
