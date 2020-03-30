using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Character))]
public class CharacterTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Character character = (Character) target;

        DrawDefaultInspector();

        if (GUILayout.Button("Find Path"))
        {
            character.FindPath(character.GetComponent<CharacterMotor>().destinationCell);
        }
    }
}
