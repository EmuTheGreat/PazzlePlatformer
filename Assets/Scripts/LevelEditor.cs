using UnityEngine;

[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour
{
    public GameObject ObjectsToCollide;

    [Header("Grid Settings")]
    public int gridWidth = 32;
    public int gridHeight = 21;
    public Vector2 tileSize = new Vector2(1f, 1f);
    public Vector2 tileSpacing = new Vector2(1f, 1f);

    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs;
    public GameObject[] tiles;
    public GameObject[] surfacesTiles;

    [Header("Placement Options")]
    public GameObject[] levelLayout;

    private void OnValidate()
    {
        if (levelLayout == null || levelLayout.Length != gridWidth * gridHeight)
        {
            levelLayout = new GameObject[gridWidth * gridHeight];
        }
    }

    public void CreateLevel()
    {
        ClearLevel();

        int keyCount = 0;

        // Находим объект KeyLevelControl
        var keyLevelControl = FindObjectOfType<KeyLevelControl>();
        Transform keyParent = keyLevelControl != null ? keyLevelControl.transform : null;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = GetPositionFromGrid(x, y);
                GameObject prefab = levelLayout[GetArrayIndex(x, y)];

                if (prefab != null && prefab.name.StartsWith("Tile"))
                {
                    prefab = levelLayout[GetArrayIndex(x, y + 1)] == null || levelLayout[GetArrayIndex(x, y + 1)].CompareTag("key_object")
                        ? surfacesTiles[Random.Range(0, surfacesTiles.Length)]
                        : tiles[Random.Range(0, tiles.Length)];
                }

                if (prefab != null)
                {
                    GameObject instantiated = InstantiateTile(prefab, position, keyParent);

                    // Проверяем тег ключа
                    if (instantiated != null && instantiated.CompareTag("key_object"))
                    {
                        keyCount++;
                    }
                }
            }
        }

        FillEmptySpaceUnderLevel();

        // Устанавливаем количество ключей в KeyLevelControl
        if (keyLevelControl != null)
        {
            keyLevelControl.totalKeys = keyCount;
        }
    }

    public void ClearLevel()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        var keyLevelControl = FindObjectOfType<KeyLevelControl>();
        for (int i = keyLevelControl.transform.childCount - 1; i >=0; i--)
        {
            DestroyImmediate(keyLevelControl.transform.GetChild(i).gameObject);
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

    private GameObject InstantiateTile(GameObject prefab, Vector3 position, Transform keyParent)
    {
        if (prefab == null) return null;

        GameObject tileObject = Instantiate(prefab, position, Quaternion.identity);
        tileObject.transform.localScale = tileSize;

        // Если объект является ключом, устанавливаем его родителем KeyLevelControl
        if (tileObject.CompareTag("key_object") && keyParent != null)
        {
            tileObject.transform.SetParent(keyParent);
        }
        else if (tileObject.CompareTag("enemy") && ObjectsToCollide.transform != null)
        {
            tileObject.transform.SetParent(ObjectsToCollide.transform);
        }
        else
        {
            tileObject.transform.SetParent(transform);
        }

        return tileObject;
    }

    private void FillEmptySpaceUnderLevel()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = -1; y > -6; y--)
            {
                Vector3 position = GetPositionFromGrid(x, y);
                GameObject prefab = tiles[Random.Range(0, tiles.Length)];
                InstantiateTile(prefab, position, null);
            }
        }
    }
}
