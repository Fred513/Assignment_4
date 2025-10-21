using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public int points = 50; // ��������������

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            GameManager.Instance.FrightenGhosts(); // �ù����뺦��״̬���¹��ܣ�
            Destroy(gameObject);
        }
    }
}
