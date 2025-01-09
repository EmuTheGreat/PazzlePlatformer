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

        // ���������� ������ ��� ������ ��������� �� �����
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

        // ���������� ������ ��� ������ �����
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

        // ���������� �����������
        EditorGUILayout.LabelField("��������", EditorStyles.boldLabel);
        if (GUILayout.Button("������� ���������"))
        {
            controller.CreateCharacter();
        }

        if (GUILayout.Button("�������� ���� ���������� ���������"))
        {
            controller.ChangeSelectedCharacterSkin();
        }

        if (GUILayout.Button("�������� ���� ���� ����������"))
        {
            controller.ChangeAllCharacterSkin();
        }

        if (GUILayout.Button("������� ���������� ���������"))
        {
            controller.RemoveCharacter(controller.selectedCharacter);
        }

        if (GUILayout.Button("������� ���� ���������� �� �����"))
        {
            controller.ClearCharacters();
        }

        serializedObject.ApplyModifiedProperties();
    }
}