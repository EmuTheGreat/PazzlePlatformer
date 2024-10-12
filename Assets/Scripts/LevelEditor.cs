using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static float cellSize = 1f;
    public static Vector2 GetPositionFromGrid(int x, int y)
    {
        // �������� ������� ����� �� ������ ������ ��� ��������� �������� �������
        float posX = x * cellSize;
        float posY = y * cellSize;

        return new Vector2(posX, posY);
    }
}
