using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static float cellSize = 1f;
    public static Vector2 GetPositionFromGrid(int x, int y)
    {
        // Умножаем индексы сетки на размер клетки для получения реальной позиции
        float posX = x * cellSize;
        float posY = y * cellSize;

        return new Vector2(posX, posY);
    }
}
