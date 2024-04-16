using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    
    private void Start()
    {
        Instantiate(_playerPrefab, transform.position, transform.rotation);
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var style = new GUIStyle();
        style.normal.textColor = new Color(0.0f, 0.5f, 0.0f);
        Handles.Label(transform.position, "Player Spawn Point", style);
    }
    #endif
}
