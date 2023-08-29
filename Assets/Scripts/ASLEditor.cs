using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ASL))]
public class ASLEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //ASL mp = (ASL)target;

        //mp.damage = EditorGUILayout.IntSlider("Damage", mp.damage, 0, 100);
        //ProgressBar(mp.damage / 100.0f, "Damage");

        //mp.armor = EditorGUILayout.IntSlider("Armor", mp.armor, 0, 100);
        //ProgressBar(mp.armor / 100.0f, "Armor");

        //bool allowSceneObjects = !EditorUtility.IsPersistent(target);
        //mp.gun = (GameObject)EditorGUILayout.ObjectField("Gun Object", mp.gun, typeof(GameObject), allowSceneObjects);
    }

    // Custom GUILayout progress bar.
    void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}
