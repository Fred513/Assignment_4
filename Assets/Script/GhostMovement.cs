using UnityEngine;

public enum GhostState { Chase, Scatter, Frightened, Dead }

public class Ghost : MonoBehaviour
{
    public GhostState state = GhostState.Scatter;
    public Transform target;          // 玩家（Pac-Man）
    public Transform ghostHome;      // 鬼魂的复活点（基地）
    public float speed = 3f;

    void Update()
    {
        switch (state)
        {
            case GhostState.Chase:
                MoveTowards(target.position);
                break;
            case GhostState.Scatter:
                MoveTowards(GetScatterCorner());
                break;
            case GhostState.Frightened:
                MoveRandom();
                break;
            case GhostState.Dead:
                MoveTowards(ghostHome.position);
                break;
        }
    }

    void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void MoveRandom()
    {
        Vector3 randomDir = Random.insideUnitCircle.normalized;
        transform.Translate(randomDir * speed * Time.deltaTime);
    }

    Vector3 GetScatterCorner()
    {
        // 每个鬼魂可以设置不同角落，这里只是示例
        return new Vector3(10, 10, 0);
    }

    // ⚡ 可选：被能量豆触发后进入害怕模式
    public void SetFrightened()
    {
        state = GhostState.Frightened;
        speed = 2f; // 害怕时速度变慢
    }

    // ⚰️ 被吃掉后复活逻辑
    public void Respawn()
    {
        transform.position = ghostHome.position;
        state = GhostState.Scatter;
        speed = 3f;
    }
}
