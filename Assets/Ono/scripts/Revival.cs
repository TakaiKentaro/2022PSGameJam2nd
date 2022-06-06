using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    [SerializeField] Transform _deat;


    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = _deat.position;
    }
}
