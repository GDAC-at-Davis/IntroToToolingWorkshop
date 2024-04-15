using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Simple sticky notes using handles 
/// </summary>
public class StickyNote : MonoBehaviour
{
    [SerializeField, TextArea()]
    private string _note;
    
    [SerializeField]
    private Color _textColor = Color.white;
    
    private void Awake()
    {
        
        // The code inside this #if will only be used when NOT IN the editor (aka in the final build)
        // Sticky notes are for the developer only! They should not be in the final build
#if !UNITY_EDITOR
        Destroy(gameObject);
#endif
    }
    
    // The code inside this #if will only be used IN the editor
    // If you don't include this then the build will fail!
    // This only applies if you use Handles (Gizmos are fine)
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // GUIStyle is what Handles uses to specify how the text looks like
        // See https://docs.unity3d.com/ScriptReference/GUIStyle.html
        var style = new GUIStyle();
        style.normal.textColor = _textColor;
        style.alignment = TextAnchor.UpperCenter;
        
        var stickyNotePosition = transform.position + Vector3.down * 0.25f;
        
        // Draw the sticky note
        Handles.Label(stickyNotePosition, _note, style);
    }
#endif

}
