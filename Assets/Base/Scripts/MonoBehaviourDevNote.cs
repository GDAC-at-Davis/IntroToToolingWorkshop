using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MonoBehaviourDevNote : MonoBehaviour
{
    [SerializeField, TextArea(3, 10)]
    private string _devNote;
}
