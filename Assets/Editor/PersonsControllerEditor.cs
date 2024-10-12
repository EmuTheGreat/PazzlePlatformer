using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonsController))]
public class PersonsControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PersonsController controller = (PersonsController)target;

        if (GUILayout.Button("Create and Add Object"))
        {
            controller.CreateCharacter();
        }
    }
}
