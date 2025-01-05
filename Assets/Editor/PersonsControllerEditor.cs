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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("personsList"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerPrefabs"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("persons"));

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

        if (GUILayout.Button("Create Character"))
        {
            controller.CreateCharacter();
        }

        if (GUILayout.Button("Change All Character Skins"))
        {
            controller.ChangeAllCharacterSkin();
        }

        if (GUILayout.Button("Clear Characters"))
        {
            controller.ClearCharacters();
        }

        serializedObject.ApplyModifiedProperties();
    }
}