using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GhostSpawner : MonoBehaviour
{
    public GameObject[] ghostPrefabs; 
    public Transform spawnPoint;      
    public float spawnInterval = 5f;   

    private float timer = 0f;
    private GameObject[] activeGhosts; 
    private Tilemap wallTilemap;      

    void Start()
    {
        activeGhosts = new GameObject[ghostPrefabs.Length];

        if (wallTilemap == null)
        {
            wallTilemap = GameObject.FindFirstObjectByType<Tilemap>();
            
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    void SpawnGhost()
    {
        if (ghostPrefabs.Length == 0 || spawnPoint == null || wallTilemap == null) return;

        int[] availableIndexes = GetAvailableIndexes();
        if (availableIndexes.Length == 0) return;

        int rand = Random.Range(0, availableIndexes.Length);
        int index = availableIndexes[rand];

        GameObject ghost = Instantiate(ghostPrefabs[index], spawnPoint.position, Quaternion.identity);
        activeGhosts[index] = ghost;

        Ghost ghostScript = ghost.GetComponent<Ghost>();
        if (ghostScript != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                ghostScript.target = player.transform;

            ghostScript.ghostHome = spawnPoint;
            ghostScript.wallTilemap = wallTilemap; // ×Ô¶¯¸³Öµ
        }
    }

    int[] GetAvailableIndexes()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < ghostPrefabs.Length; i++)
        {
            if (activeGhosts[i] == null)
                list.Add(i);
        }
        return list.ToArray();
    }
}
