//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(GodEnemy))]
//public class EGodEnemy :  Editor {

//    private bool _hasDeathEffect;
//    private bool _hasHurtEffect;

//    public override void OnInspectorGUI()
//    {
//        var script = (GodEnemy)target;

//        serializedObject.Update();

//        DrawDefaultInspector();

//        // Header.
//        GUILayout.Label("Effects", EditorStyles.boldLabel);

//        // Checkbox.
//        _hasDeathEffect = EditorGUILayout.Toggle("Enable Death Effect", _hasDeathEffect);

//        // Death effect fields.
//        if(_hasDeathEffect)
//        {
//            script.HealthManager._deathEffect = (GameObject)EditorGUILayout.ObjectField("Death Effect", script.HealthManager._deathEffect, typeof(GameObject), false);
//            script.HealthManager._deathEffectSP = (Transform)EditorGUILayout.ObjectField("Death Effect Spawn Point", script.HealthManager._deathEffectSP, typeof(Transform), true);

//            EditorUtility.SetDirty(script);
//            GUILayout.Label("");
//        }
//        else
//        {
//            script.HealthManager._deathEffect = null;
//            script.HealthManager._deathEffectSP = null;
//        }

//        // Checkbox.
//        _hasHurtEffect = EditorGUILayout.Toggle("Enable Hurt Effect", _hasHurtEffect);

//        // Hurt effect fields.
//        if (_hasHurtEffect)
//        {
//            script.HealthManager._hurtEffect = (GameObject)EditorGUILayout.ObjectField("Hurt Effect", script.HealthManager._hurtEffect, typeof(GameObject), false);
//            script.HealthManager._posionEffect = (GameObject)EditorGUILayout.ObjectField("Posion Effect", script.HealthManager._posionEffect, typeof(GameObject), false);
//            script.HealthManager._hurtEffectSP = (Transform)EditorGUILayout.ObjectField("Hurt Effect Spawn Point", script.HealthManager._hurtEffectSP, typeof(Transform), true);

//            EditorUtility.SetDirty(script);
//            GUILayout.Label("");
//        }
//        else
//        {
//            script.HealthManager._hurtEffect = null;
//            script.HealthManager._hurtEffectSP = null;
//        }

//        EditorUtility.SetDirty(script);
//    }
//}
