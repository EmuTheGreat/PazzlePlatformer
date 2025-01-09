using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonsController))]
public class PersonsControllerEditor : Editor
{
    private PersonsController controller;

    private void OnEnable()
    {
        controller = (PersonsController)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Character Management", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("personsList"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerPrefabs"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("persons"), true);

        EditorGUILayout.Space();

        // Выпадающий список для выбора персонажа на сцене
        if (controller.persons.Count > 0)
        {
            string[] characterNames = new string[controller.persons.Count];
            for (int i = 0; i < controller.persons.Count; i++)
            {
                characterNames[i] = controller.persons[i] != null ? $"{i} {controller.persons[i].name}" : "None";
            }

            int selectedCharacterIndex = EditorGUILayout.Popup("Selected Character",
                controller.persons.IndexOf(controller.selectedCharacter),
                characterNames);

            controller.selectedCharacter = selectedCharacterIndex >= 0
                ? controller.persons[selectedCharacterIndex]
                : null;
        }
        else
        {
            EditorGUILayout.HelpBox("No characters available in the scene.", MessageType.Warning);
        }

        EditorGUILayout.Space();

        // Выпадающий список для выбора скина
        if (controller.playerPrefabs.Count > 0)
        {
            string[] prefabNames = new string[controller.playerPrefabs.Count];
            for (int i = 0; i < controller.playerPrefabs.Count; i++)
            {
                prefabNames[i] = controller.playerPrefabs[i] != null ? controller.playerPrefabs[i].name : "None";
            }
            controller.SelectPrefab(EditorGUILayout.Popup("Selected Prefab/Skin", controller.selectedPrefabIndex, prefabNames));
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Character Parameters", EditorStyles.boldLabel);

        controller.objectPosition = EditorGUILayout.Vector2Field("Object Position", controller.objectPosition);
        controller.colliderSize = EditorGUILayout.Vector2Field("Collider Size", controller.colliderSize);
        controller.movementSpeed = EditorGUILayout.FloatField("Movement Speed", controller.movementSpeed);
        controller.jumpForce = EditorGUILayout.FloatField("Jump Force", controller.jumpForce);
        controller.movementDirectionReverse = EditorGUILayout.Toggle("Movement Direction Reverse", controller.movementDirectionReverse);

        EditorGUILayout.Space();

        // Управление персонажами
        EditorGUILayout.LabelField("Действия", EditorStyles.boldLabel);
        if (GUILayout.Button("Создать персонажа"))
        {
            controller.CreateCharacter();
        }

        if (GUILayout.Button("Поменять скин выбранному персонажу"))
        {
            controller.ChangeSelectedCharacterSkin();
        }

        if (GUILayout.Button("Поменять скин всем персонажам"))
        {
            controller.ChangeAllCharacterSkin();
        }

        if (GUILayout.Button("Удалить выбранного персонажа"))
        {
            controller.RemoveCharacter(controller.selectedCharacter);
        }

        if (GUILayout.Button("Удалить всех персонажей со сцены"))
        {
            controller.ClearCharacters();
        }

        serializedObject.ApplyModifiedProperties();
    }
}