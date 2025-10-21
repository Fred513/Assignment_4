using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] wallPrefabs;

    private float tileWidth;
    private float tileHeight;

    private int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    void Start()
    {
        if (wallPrefabs.Length == 0) return;

        SpriteRenderer sr = wallPrefabs[0].GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            tileWidth = sr.bounds.size.x;
            tileHeight = sr.bounds.size.y;
        }
        else
        {
            tileWidth = 1f;
            tileHeight = 1f;
        }

        GenerateFullLevel();
    }

    void GenerateFullLevel()
    {
        int rows = levelMap.GetLength(0);
        int cols = levelMap.GetLength(1);

        for (int flipV = 0; flipV <= 1; flipV++)
        {
            for (int flipH = 0; flipH <= 1; flipH++)
            {
                GenerateQuadrant(flipH == 1, flipV == 1, rows, cols);
            }
        }
    }

    void GenerateQuadrant(bool flipH, bool flipV, int rows, int cols)
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                int tile = levelMap[y, x];
                if (tile == 0) continue;

                int prefabIndex = tile - 1;
                if (prefabIndex < 0 || prefabIndex >= wallPrefabs.Length) continue;

                float posX = x * tileWidth;
                float posY = -y * tileHeight;

                if (flipH) posX = cols * tileWidth - posX - tileWidth;
                if (flipV) posY = -rows * tileHeight - posY + tileHeight;

                Vector3 pos = new Vector3(posX, posY, 0);

                float rotZ = GetRotation(x, y, tile);

                if (flipH) rotZ = 180f - rotZ;
                if (flipV) rotZ = -rotZ;

                Instantiate(wallPrefabs[prefabIndex], pos, Quaternion.Euler(0, 0, rotZ));
            }
        }
    }

    float GetRotation(int x, int y, int tile)
    {
        if (tile == 1)
        {
            bool right = (x + 1 < levelMap.GetLength(1)) && (levelMap[y, x + 1] != 0);
            bool down = (y + 1 < levelMap.GetLength(0)) && (levelMap[y + 1, x] != 0);

            if (right && down) return 0f;
            if (!right && down) return 90f;
            if (!right && !down) return 180f;
            if (right && !down) return 270f;
        }

        if (tile == 3)
        {
            bool left = (x - 1 >= 0) && (levelMap[y, x - 1] != 0);
            bool up = (y - 1 >= 0) && (levelMap[y - 1, x] != 0);

            if (left && up) return 0f;
            if (!left && up) return 90f;
            if (!left && !up) return 180f;
            if (left && !up) return 270f;
        }

        return 0f;
    }
}
