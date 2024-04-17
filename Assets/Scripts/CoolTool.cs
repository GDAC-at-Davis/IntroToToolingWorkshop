using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTool : MonoBehaviour
{
    public float explosionRadius;
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
    }
}
