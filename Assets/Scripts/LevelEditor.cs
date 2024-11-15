using UnityEngine;

[ExecuteInEditMode] // Позволяет запускать скрипт в редакторе Unity
public class LevelEditor : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 32;
    public int gridHeight = 21;
    public Vector2 tileSize = new Vector2(1f, 1f); // Размер одного тайла
    public Vector2 tileSpacing = new Vector2(1f, 1f); // Шаг между тайлами

    private int x = 0;
    private int y = 0;

    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs; // Массив префабов, которые можно размещать на уровне

    [Header("Placement Options")]
    public TileType[] levelLayout; // Одномерный массив для размещения объектов

    public enum TileType
    {
        StaticObject, // Платформа с коллизией
        BackgroundElement, // Декоративный объект без коллизии
        EnemyObject, // Враг, удаляет игрока при пересечении
        KeyObject // Ключевой объект, удаляет игрока и переходит к следующему уровню
    }

    void OnValidate()
    {
        // Обновляем размер массива при изменении ширины и высоты сетки
        if (levelLayout == null || levelLayout.Length != gridWidth * gridHeight)
        {
            levelLayout = new TileType[gridWidth * gridHeight];
        }
    }

    // Метод для создания уровня
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


    // Метод для конвертации координат x, y в одномерный индекс
    public int GetArrayIndex(int x, int y)
    {
        return y * gridWidth + x;
    }

    // Получаем тип тайла из массива
    private TileType GetTileType(int x, int y)
    {
        return levelLayout[GetArrayIndex(x, y)];
    }

    // Метод для создания тайла
    private GameObject InstantiateTile(TileType tileType, Vector3 position)
    {
        GameObject tilePrefab = GetPrefabByTileType(tileType);
        if (tilePrefab == null) return null;

        // Здесь менять parentPrefab
        GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, transform);
        return tileObject;
    }

    // Получаем префаб по типу тайла
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


    // Метод для очистки уровня
    public void ClearLevel()
    {
        // Удаление всех дочерних объектов
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    // Метод для вычисления позиции тайла по координатам x и y
    public Vector3 GetPositionFromGrid(int x, int y)
    {
        // Рассчитываем начальное смещение для центрирования уровня
        float startX = -(gridWidth * (tileSize.x + tileSpacing.x)) / 2 + tileSize.x / 2;
        float startY = -(gridHeight * (tileSize.y + tileSpacing.y)) / 2 + tileSize.y / 2;

        // Вычисляем позицию каждого тайла с учетом смещения от центра
        float posX = startX + x * (tileSize.x + tileSpacing.x);
        float posY = startY + y * (tileSize.y + tileSpacing.y);

        return new Vector3(posX, posY, 0);
    }

}