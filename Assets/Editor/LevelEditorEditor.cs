using UnityEditor;
using UnityEngine;
using static LevelEditor;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorEditor : Editor
{
    private LevelEditor levelEditor;

    private void OnEnable()
    {
        levelEditor = (LevelEditor)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Create Level"))
        {
            // ���������, ��� ������� ��������� ������ ���� ��� ��� ������� �� ������
            levelEditor.CreateLevel();
        }

        GUILayout.Space(10);

        // ������ ����� � ������ � �������������
        for (int y = levelEditor.gridHeight - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < levelEditor.gridWidth; x++)
            {
                // ������ ������ ��� ������ ������ �������
                int index = levelEditor.GetArrayIndex(x, y);
                TileType tileType = levelEditor.levelLayout[index];
                string buttonLabel = tileType.ToString();

                // �������� ������ ��� ����, �� ������� ������, ��� ������������ ������
                if (GUILayout.Button(buttonLabel, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    // ��������� ��������� ���� �����
                    TileType newTileType = (TileType)(((int)tileType + 1) % System.Enum.GetValues(typeof(TileType)).Length);
                    levelEditor.levelLayout[index] = newTileType;

                    // ��������� ���������� ������������� ������
                    levelEditor.CreateLevel();
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}

