#if UNITY_EDITOR
/*==================*\
|*   Unity Usings   *|
\*==================*/

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

/*=========================*\
|*   CLASS: ExplodeOnHit   *|
\*=========================*/

[CanEditMultipleObjects]
[CustomEditor(typeof(ExplodeOnHit), true)]
public class ExplodeOnHitEditor : Editor
{
    /*=====================*\
    |*   Unity Functions   *|
    \*=====================*/

    void OnEnable()
    {
        m_tag   = serializedObject.FindProperty("m_tag");
        m_index = serializedObject.FindProperty("m_index");
    }
    public override void OnInspectorGUI()
    {
        // Update serializedObject
        // -----------------------
        serializedObject.Update();

        // Draw default script
        // -------------------
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(target as ExplodeOnHit), typeof(ExplodeOnHit), false);

        // Draw Tag dropdown
        // -----------------
        m_index.intValue = EditorGUILayout.Popup("Tag", m_index.intValue, InternalEditorUtility.tags);
        m_tag.stringValue = InternalEditorUtility.tags[m_index.intValue];

        // Apply modifications
        // -------------------
        serializedObject.ApplyModifiedProperties();

        // Draw Default Inspector
        // ----------------------
        DrawPropertiesExcluding(serializedObject, new string[] { "m_Script" } );

        // Apply modifications
        // -------------------
        serializedObject.ApplyModifiedProperties();
    }

    /*==============================*\
    |*   Private Member Variables   *|
    \*==============================*/

        /*====================*\
        |*   Runtime memory   *|
        \*====================*/

        private SerializedProperty m_tag   = null;
        private SerializedProperty m_index = null;
}

#endif
