using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    [SerializeField] Transform _deat;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = _deat.position;
        }
    }
}
