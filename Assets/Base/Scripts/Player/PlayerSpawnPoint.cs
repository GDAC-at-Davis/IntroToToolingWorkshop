using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    
    private void Awake()
    {
        var player = Instantiate(_playerPrefab, transform.position, transform.rotation);
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var style = new GUIStyle();
        style.normal.textColor = new Color(0.0f, 0.2f, 0.8f);
        Handles.Label(transform.position, "Player Spawn Point", style);
    }
    #endif
}
