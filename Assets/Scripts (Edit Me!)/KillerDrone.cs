using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KillerDrone : MonoBehaviourDevNote
{
    /* Use the variables here to draw tool UI */
    public float AttackRadius;
    public float ExplodingDistance;
    public float MoveSpeed;

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
    #region Drone Behavior
    
    [Header("Dependencies"), SerializeField]
    private ParticleSystem _explosion;
    
    [SerializeField]
    private GameObject _mesh;

    private PlayerEntity _player;

    private bool _lockedOn;
    private bool _exploded;
    
    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerEntity>();
    }

    private void Update()
    {
        if (_exploded)
        {
            return;
        }
        
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        
        if (distance < AttackRadius)
        {
            _lockedOn = true;
        }
        
        if (distance < ExplodingDistance)
        {
            _mesh.SetActive(false);
            _explosion.Play();
            _player.Hit();
            _exploded = true;
        }
        
        if (_lockedOn)
        {
            var direction = (_player.transform.position - transform.position).normalized;
            transform.position += direction * MoveSpeed * Time.deltaTime;
            
            // face the player
            transform.forward = direction;
        }
    }

    #endregion
}
