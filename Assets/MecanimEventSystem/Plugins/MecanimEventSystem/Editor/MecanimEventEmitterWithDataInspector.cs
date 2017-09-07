using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

//using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor (typeof(MecanimEventEmitterWithData))]
public class MecanimEventEmitterWithDataInspector : Editor
{
    SerializedProperty controller;
    SerializedProperty animator;
    SerializedProperty data;

    void OnEnable ()
    {
        controller = serializedObject.FindProperty ("animatorController");
        animator = serializedObject.FindProperty ("animator");
        data = serializedObject.FindProperty ("data");
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.UpdateIfDirtyOrScript ();
		
        EditorGUILayout.PropertyField (animator);
		
        if (animator.objectReferenceValue != null) {
            AnimatorController animatorController = AnimatorControllerExtension.GetEffectiveAnimatorController ((Animator)animator.objectReferenceValue);
            controller.objectReferenceValue = animatorController;
        } else {
            controller.objectReferenceValue = null;
        }
		
        EditorGUILayout.ObjectField ("AnimatorController", controller.objectReferenceValue, typeof(AnimatorController), false);
		
        EditorGUILayout.PropertyField (data);

        serializedObject.ApplyModifiedProperties ();
    }
}