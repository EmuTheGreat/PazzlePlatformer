using UnityEngine;

[ExecuteInEditMode] // ��������� ��������� ������ � ��������� Unity
public class LevelEditor : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 32;
    public int gridHeight = 21;
    public Vector2 tileSize = new Vector2(1f, 1f); // ������ ������ �����
    public Vector2 tileSpacing = new Vector2(1f, 1f); // ��� ����� �������

    private int x = 0;
    private int y = 0;

    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs; // ������ ��������, ������� ����� ��������� �� ������

    [Header("Placement Options")]
    public TileType[] levelLayout; // ���������� ������ ��� ���������� ��������

    public enum TileType
    {
        StaticObject, // ��������� � ���������
        BackgroundElement, // ������������ ������ ��� ��������
        EnemyObject, // ����, ������� ������ ��� �����������
        KeyObject // �������� ������, ������� ������ � ��������� � ���������� ������
    }

    void OnValidate()
    {
        // ��������� ������ ������� ��� ��������� ������ � ������ �����
        if (levelLayout == null || levelLayout.Length != gridWidth * gridHeight)
        {
            levelLayout = new TileType[gridWidth * gridHeight];
        }
    }

    // ����� ��� �������� ������
    public void CreateLevel()
    {
        ClearLevel();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = GetPositionFromGrid(x, y);
                GameObject tileObject = InstantiateTile(GetTileType(x, y), position);
                if (tileObject != null)
                {
                    tileObject.transform.localScale = new Vector3(tileSize.x, tileSize.y, 1);
                }
            }
        }
    }


    // ����� ��� ����������� ��������� x, y � ���������� ������
    public int GetArrayIndex(int x, int y)
    {
        return y * gridWidth + x;
    }

    // �������� ��� ����� �� �������
    private TileType GetTileType(int x, int y)
    {
        return levelLayout[GetArrayIndex(x, y)];
    }

    // ����� ��� �������� �����
    private GameObject InstantiateTile(TileType tileType, Vector3 position)
    {
        GameObject tilePrefab = GetPrefabByTileType(tileType);
        if (tilePrefab == null) return null;

        // ����� ������ parentPrefab
        GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, transform);
        return tileObject;
    }

    // �������� ������ �� ���� �����
    private GameObject GetPrefabByTileType(TileType tileType)
    {
        if (tilePrefabs == null || tilePrefabs.Length == 0) return null;

        switch (tileType)
        {
            case TileType.StaticObject:
                return tilePrefabs[0];
            case TileType.BackgroundElement:
                return tilePrefabs[1];
            case TileType.EnemyObject:
                return tilePrefabs[2];
            case TileType.KeyObject:
                return tilePrefabs[3];
            default:
                return null;
        }
    }


    // ����� ��� ������� ������
    public void ClearLevel()
    {
        // �������� ���� �������� ��������
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    // ����� ��� ���������� ������� ����� �� ����������� x � y
    public Vector3 GetPositionFromGrid(int x, int y)
    {
        // ������������ ��������� �������� ��� ������������� ������
        float startX = -(gridWidth * (tileSize.x + tileSpacing.x)) / 2 + tileSize.x / 2;
        float startY = -(gridHeight * (tileSize.y + tileSpacing.y)) / 2 + tileSize.y / 2;

        // ��������� ������� ������� ����� � ������ �������� �� ������
        float posX = startX + x * (tileSize.x + tileSpacing.x);
        float posY = startY + y * (tileSize.y + tileSpacing.y);

        return new Vector3(posX, posY, 0);
    }

}