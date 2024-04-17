using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private string _nextLevel;
    
    private void OnTriggerEnter(Collider other)
    {
        var entity = other.gameObject.GetComponent<PlayerEntity>();
        if (entity != null)
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var style = new GUIStyle();
        style.normal.textColor = new Color(0.0f, 0.8f, 0.2f);
        Handles.Label(transform.position, "Level Goal", style);
        
        var boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Gizmos.color = new Color(0.0f, 1f, 0.2f);
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
#endif
}
