using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalCheck : MonoBehaviour
{
    [Tooltip("�X�^�[�g��������������")] public bool _sCheck = false;
    
    public void Signal()
    {
        _sCheck = true;
    }
}
