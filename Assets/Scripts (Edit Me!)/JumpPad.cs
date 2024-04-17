using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviourDevNote
{
    /* Use the variables here to draw tool UI */
    public float JumpHeight;
    public SphereCollider JumpPadTrigger;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // DRAW YOUR TOOL UI HERE!
    }

    private void OnDrawGizmosSelected()
    {
        // DRAW YOUR ON SELECTION TOOL UI HERE!
    }

#endif
    
    /* You can ignore everything below this line for the workshop */
    #region Jump Pad Behavior

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.gameObject.GetComponent<PlayerEntity>();
        if (entity != null)
        {
            entity.Launch(JumpHeight);
        }
    }

    #endregion
}
