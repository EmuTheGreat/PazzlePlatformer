using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonsController))]
public class PersonsControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PersonsController controller = (PersonsController)target;

        if (GUILayout.Button("Create Character"))
        {
            controller.CreateCharacter();
        }

        if (GUILayout.Button("Change All Skins"))
        {
            controller.ChangeAllCharacterSkin();
        }

        if (GUILayout.Button("Change Skin by Index"))
        {
            controller.ChangeCharacterSkin();
        }

        controller.index = EditorGUILayout.IntField("Index", controller.index);
    }
}
