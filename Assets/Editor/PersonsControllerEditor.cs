using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonsController))]
public class PersonsControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PersonsController controller = (PersonsController)target;
        serializedObject.Update();

        // ������������ �������, ��������� � ���������
        EditorGUILayout.PropertyField(serializedObject.FindProperty("collideObjectsList"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("personsList"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerPrefabForChangeSkin"));

        // ������������ "�������� ���������"
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("�������� ���������", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("objects"), true);
        controller.objectPosition = EditorGUILayout.Vector2Field("Object Position", controller.objectPosition);
        controller.colliderSize = EditorGUILayout.Vector2Field("Collider Size", controller.colliderSize);

        controller.movementSpeed = EditorGUILayout.FloatField("Movement Speed", controller.movementSpeed);
        controller.jumpForce = EditorGUILayout.FloatField("Jump Force", controller.jumpForce);

        controller.moveLeftKey = (KeyCode)EditorGUILayout.EnumPopup("Move Left Key", controller.moveLeftKey);
        controller.moveRightKey = (KeyCode)EditorGUILayout.EnumPopup("Move Right Key", controller.moveRightKey);
        controller.jumpKey = (KeyCode)EditorGUILayout.EnumPopup("Jump Key", controller.jumpKey);
        controller.specialActionKey = (KeyCode)EditorGUILayout.EnumPopup("Special Action Key", controller.specialActionKey);

        controller.movementDirectionReverse = EditorGUILayout.Toggle("Movement Direction Reverse", controller.movementDirectionReverse);

        // ������ "Create Character" ����� ����� ����� "�������� ���������"
        EditorGUILayout.Space();
        if (GUILayout.Button("Create Character"))
        {
            controller.CreateCharacter();
        }

        // ���������� ��������� "��������� ��������"
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��������� ��������", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("collisionObjects"), true);
        controller.removeOnCollision = EditorGUILayout.Toggle("Remove On Collision", controller.removeOnCollision);

        // ������ "Change All Skins"
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("����� �����", EditorStyles.boldLabel);
        if (GUILayout.Button("Change All Skins"))
        {
            controller.ChangeAllCharacterSkin();
        }

        // ���� index � ������ "Change Skin by Index"
        controller.index = EditorGUILayout.IntField("Index", controller.index);
        if (GUILayout.Button("Change Skin by Index"))
        {
            controller.ChangeCharacterSkin();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
