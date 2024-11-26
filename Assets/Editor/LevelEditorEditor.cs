using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorEditor : Editor
{
    private LevelEditor levelEditor;
    private GameObject selectedPrefab;

    private const int ButtonSize = 20;

    private void OnEnable()
    {
        levelEditor = (LevelEditor)target;
        selectedPrefab = null;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Create Level"))
        {
            levelEditor.CreateLevel();
        }

        if (GUILayout.Button("Clear Level"))
        {
            levelEditor.ClearLevel();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }

        GUILayout.Space(10);

        GUILayout.Label("Select Prefab for Level:");

        if (GUILayout.Button("Select Prefab"))
        {
            // Открываем меню выбора префаба
            GenericMenu menu = new GenericMenu();

            // Добавляем все префабы в меню
            for (int i = 0; i < levelEditor.tilePrefabs.Length; i++)
            {
                int prefabIndex = i;
                GameObject prefab = levelEditor.tilePrefabs[i];
                menu.AddItem(new GUIContent(prefab.name), false, () =>
                {
                    selectedPrefab = prefab;
                    Debug.Log("Selected prefab: " + selectedPrefab.name);
                });
            }

            menu.AddItem(new GUIContent("Clear"), false, () =>
            {
                selectedPrefab = null;
                Debug.Log("Selected prefab cleared.");
            });

            menu.ShowAsContext();
        }

        GUILayout.Space(10);

        GUILayout.Label("Grid Editor");

        GUILayout.Space(10);

        // Рисуем сетку
        for (int y = levelEditor.gridHeight - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < levelEditor.gridWidth; x++)
            {
                int index = levelEditor.GetArrayIndex(x, y);
                GameObject currentPrefab = levelEditor.levelLayout[index];

                string buttonLabel = currentPrefab != null ? currentPrefab.name : "-";

                if (GUILayout.Button(buttonLabel, GUILayout.Width(ButtonSize), GUILayout.Height(ButtonSize)))
                {
                    levelEditor.levelLayout[index] = selectedPrefab;
                    EditorUtility.SetDirty(levelEditor);
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    private void ClearGrid()
    {
        for (int i = 0; i < levelEditor.levelLayout.Length; i++)
        {
            levelEditor.levelLayout[i] = null;
        }
        EditorUtility.SetDirty(levelEditor);
    }
}

