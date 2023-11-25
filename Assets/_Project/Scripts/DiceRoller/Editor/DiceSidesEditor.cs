using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DiceSides))]
public class DiceSidesEditor : Editor {
    SerializedProperty diceSides;
    
    void OnEnable() {
        diceSides = serializedObject.FindProperty("Sides");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        
        ShowDiceSideInspectorGUI();
        
        serializedObject.ApplyModifiedProperties();
        
        if (GUILayout.Button("Calculate Sides")) {
            CalculateSides();
        }
    }
    
    void ShowDiceSideInspectorGUI() {
        EditorGUILayout.LabelField("Dice Editor", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        for (int i = 0; i < diceSides.arraySize; i++) {
            ShowDiceSideUI(i);
        }
        EditorGUI.indentLevel--;
    }

    void ShowDiceSideUI(int index) {
        SerializedProperty side = diceSides.GetArrayElementAtIndex(index);
        SerializedProperty value = side.FindPropertyRelative("Value");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(value, new GUIContent("Side " + (index + 1)));

        if (GUILayout.Button("Show", GUILayout.Width(70))) {
            RotateDiceToSide(index);
        }
        
        EditorGUILayout.EndHorizontal();
    }
    
    void RotateDiceToSide(int index) {
        DiceSides sides = target as DiceSides;
        sides.transform.rotation = sides.GetWorldRotationFor(index);
        SceneView.RepaintAll();
    }

    void CalculateSides() {
        DiceSides sides = target as DiceSides;
        Mesh mesh = GetMesh(sides);

        List<DiceSide> foundSides = FindDiceSides(mesh);
        sides.Sides = new DiceSide[foundSides.Count];
        serializedObject.Update();

        for (int i = 0; i < foundSides.Count; i++) {
            DiceSide side = foundSides[i];
            SerializedProperty sideProperty = diceSides.GetArrayElementAtIndex(i);
            sideProperty.FindPropertyRelative("Center").vector3Value = side.Center;
            sideProperty.FindPropertyRelative("Normal").vector3Value = side.Normal;
        }
        
        serializedObject.ApplyModifiedProperties();
        sides.transform.rotation = sides.GetWorldRotationFor(0);
    }

    List<DiceSide> FindDiceSides(Mesh mesh) {
        List<DiceSide> result = new();

        int[] triangles = mesh.GetTriangles(0);
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        
        for (int i = 0; i < triangles.Length; i += 3) {
            Vector3 a = vertices[triangles[i]];
            Vector3 b = vertices[triangles[i + 1]];
            Vector3 c = vertices[triangles[i + 2]];

            result.Add(new DiceSide {
                Center = (a + b + c) / 3f,
                Normal = Vector3.Cross(b - a, c - a).normalized
            });
        }
        
        return result;
    }

    Mesh GetMesh(DiceSides sides) {
        MeshCollider meshCollider = sides.GetComponent<MeshCollider>();
        if (meshCollider != null) {
            return meshCollider.sharedMesh;
        } else {
            MeshFilter meshFilter = sides.GetComponent<MeshFilter>();
            return meshFilter.sharedMesh;
        }
    }
}