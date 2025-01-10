using UnityEngine;

[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 32;
    public int gridHeight = 21;
    public Vector2 tileSize = new Vector2(1f, 1f); // Размер одного тайла
    public Vector2 tileSpacing = new Vector2(1f, 1f); // Шаг между тайлами

    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs; // Массив префабов, которые можно размещать на уровне

    [Header("Placement Options")]
    public GameObject[] levelLayout; // Одномерный массив для размещения объектов

    public GameObject[] tiles;

    public GameObject[] surfacesTiles;

    void OnValidate()
    {
        if (levelLayout == null || levelLayout.Length != gridWidth * gridHeight)
        {
            levelLayout = new GameObject[gridWidth * gridHeight];
        }
    }

    public void CreateLevel()
    {
        ClearLevel();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = GetPositionFromGrid(x, y);
                GameObject prefab = levelLayout[GetArrayIndex(x, y)];

                if (prefab != null && prefab.name.StartsWith("Tile"))
                {
                    if (levelLayout[GetArrayIndex(x, y + 1)] == null)
                    {
                        prefab = surfacesTiles[Random.Range(0, surfacesTiles.Length)];
                    }
                    else
                    {
                        prefab = tiles[Random.Range(0, tiles.Length)];
                    }
                }

                if (prefab != null)
                {
                    InstantiateTile(prefab, position);
                }
            }
        }
        FillEmptySpaceUnderLevel();
    }

    public void ClearLevel()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public Vector3 GetPositionFromGrid(int x, int y)
    {
        float startX = -(gridWidth * (tileSize.x + tileSpacing.x)) / 2 + tileSize.x / 2;
        float startY = -(gridHeight * (tileSize.y + tileSpacing.y)) / 2 + tileSize.y / 2;

        float posX = startX + x * (tileSize.x + tileSpacing.x);
        float posY = startY + y * (tileSize.y + tileSpacing.y);

        return new Vector3(posX, posY, 0);
    }

    public int GetArrayIndex(int x, int y)
    {
        return y * gridWidth + x;
    }

    private GameObject InstantiateTile(GameObject prefab, Vector3 position)
    {
        if (prefab == null) return null;

        GameObject tileObject = Instantiate(prefab, position, Quaternion.identity, transform);

        Renderer renderer = tileObject.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Vector3 originalSize = renderer.bounds.size; // Фактические размеры объекта
            Vector3 prefabOriginalScale = prefab.transform.localScale; // Исходный масштаб объекта

            float scaleX = tileSize.x / originalSize.x;
            float scaleY = tileSize.y / originalSize.y;
            float scale = Mathf.Min(scaleX, scaleY);

            tileObject.transform.localScale = new Vector3(
                prefabOriginalScale.x * scale,
                prefabOriginalScale.y * scale,
                prefabOriginalScale.z * scale // Масштабируем Z для корректного восприятия
            );

            float offsetY = (tileSize.y - tileObject.transform.localScale.y) / 2;

            tileObject.transform.position = new Vector3(
                tileObject.transform.position.x,
                tileObject.transform.position.y - offsetY,
                tileObject.transform.position.z);
        }

        return tileObject;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = GetPositionFromGrid(x, y);
                Vector3 size = new Vector3(tileSize.x, tileSize.y, 0);
                Gizmos.DrawWireCube(position, size);
            }
        }
    }

    private void FillEmptySpaceUnderLevel()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = -1; y > -6; y--)
            {
                Vector3 position = GetPositionFromGrid(x, y);
                GameObject prefab = prefab = tiles[Random.Range(0, tiles.Length)];

                InstantiateTile(prefab, position);

            }
        }
    }
}