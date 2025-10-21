using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            gameObject.SetActive(false);
        }
    }
}
