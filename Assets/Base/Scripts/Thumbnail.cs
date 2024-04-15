using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SimpleDemo : MonoBehaviour
{
    [SerializeField]
    private float _funkyZoneSize;
    
    // The code inside this #if will only be used IN the editor
    // If you don't include this then the build will fail!
    // This only applies if you use Handles (Gizmos are fine)
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // All gizmos drawn after this will be red (until set to another color)
        Gizmos.color = Color.red;
        
        // This basically just means that the gizmos will be drawn in the local space of the object
        Gizmos.matrix = transform.localToWorldMatrix;
        
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * _funkyZoneSize);
        
        // Reset the matrix to identity so that the gizmos drawn after this will be in world space
        Gizmos.matrix = Matrix4x4.identity;
        
        Handles.Label(transform.position, "FunkyZone");
    }
#endif
}
