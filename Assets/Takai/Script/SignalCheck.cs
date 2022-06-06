using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalCheck : MonoBehaviour
{
    [Tooltip("スタートが完了した判定")] public bool _sCheck = false;
    
    public void Signal()
    {
        _sCheck = true;
    }
}
