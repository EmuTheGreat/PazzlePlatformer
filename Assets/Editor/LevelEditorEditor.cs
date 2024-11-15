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
            // Убедитесь, что уровень создается только один раз при нажатии на кнопку
            levelEditor.CreateLevel();
        }

        GUILayout.Space(10);

        // Рисуем сетку и делаем её интерактивной
        for (int y = levelEditor.gridHeight - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < levelEditor.gridWidth; x++)
            {
                // Создаём кнопку для каждой ячейки массива
                int index = levelEditor.GetArrayIndex(x, y);
                TileType tileType = levelEditor.levelLayout[index];
                string buttonLabel = tileType.ToString();

                // Изменяем только тот тайл, на который нажали, без пересоздания уровня
                if (GUILayout.Button(buttonLabel, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    // Обработка изменения типа тайла
                    TileType newTileType = (TileType)(((int)tileType + 1) % System.Enum.GetValues(typeof(TileType)).Length);
                    levelEditor.levelLayout[index] = newTileType;

                    // Обновляем визуальное представление уровня
                    levelEditor.CreateLevel();
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}

