using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("L�ERshift�Ŕ�����Ƃ�")] string _shiftName;

    [Tooltip("�v���C���[��Rigidbody")]
    Rigidbody _rb;

    [Tooltip("�v���C���[�̃g�����X�t�H�[��")]
    Transform _transform;

    [Tooltip("�v���C���[�ɉ������")]
    [SerializeField] Vector3 _playerForcePower;
    [Tooltip("�O�i�L�[�𗣂��Ă���Ԃ̌�����")]
    [SerializeField] Vector3 _playerDeceleratePower;

    [Tooltip("�`�F�b�N�|�C���g�̔z��")]
    [SerializeField] Transform[] _chedkPoint;

    [Tooltip("���݂̑��x")]
    public float _nowSpeed1;
    [Tooltip("���݂̑��x")]
    public float _nowSpeed2;

    Transform _nowPoint = default;
    Transform _nextPoint = default;
    /// <summary>���̃`�F�b�N�|�C���g�̃C���f�b�N�X���w��</summary>
    int _checkPointManage = 1;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        //null�`�F�b�N
        if (_rb == null)
        {
            Debug.Log("�v���C���[��Rigitbody���A�T�C������Ă܂���");
        }
        if (_transform == null)
        {
            Debug.Log("�v���C���[2��Transform���A�T�C������Ă܂���");
        }
        if ((_nextPoint = _chedkPoint[0]) == null)
        {
            Debug.Log("�`�F�b�N�|�C���g���A�T�C������Ă��܂���");
        }


        _nowPoint = _chedkPoint[0];
    }


    void Update()
    {
        //�ړ�����
        PlayerMoveManager(_transform, _rb);
        //�\���p���x���擾����
        _nowSpeed1 = Mathf.Sqrt((_rb.velocity.x * _rb.velocity.x) + (_rb.velocity.z * _rb.velocity.z));
    }

    void PlayerMoveManager(Transform tr, Rigidbody rb)
    {
        //�C�ӂ̃{�^����������Ă���ԁA_playerForcePower��������
        if (Input.GetButton(_shiftName))
        {
            MoveCheckPoint(tr, rb, _nextPoint.GetChild(0));
        }

        //���R�[�h:�����������������悤�Ƃ���

        //��������
        ////������Ă���Ԃ͌�������
        //else if (rb.velocity.z > 0 || rb.velocity.x > 0)//�O�i�͂��c���Ă����猸��
        //{
        //    //rb.AddForce(rb.velocity.x, rb.velocity.y, _playerDeceleratePower.z);
        //}
        //else if ((rb.velocity.z <= 0) || (rb.velocity.x <= 0))//��i�͂��Ȃ�
        //{
        //    //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        //}
        //�����܂�
    }


    void MoveCheckPoint(Transform pt, Rigidbody rb, Transform nextPosition)
    {
        //�v���C���[����`�F�b�N�|�C���g�܂ł̕����x�N�g�����擾����
        Vector3 directionVector = nextPosition.position - pt.position;

        //�����x�N�g�����m�[�}���C�Y��
        directionVector = directionVector.normalized;


        //�͂̌����Ƌ������|����
        Vector3 newForce = default;
        newForce.x = directionVector.x * _playerForcePower.x;
        newForce.z = directionVector.z * _playerForcePower.z;
        //Debug.Log(newForce);

        rb.AddForce(newForce.x, rb.velocity.y, newForce.z);
    }

    void ChangeCheckPoint()
    {
        //�w�肳�ꂽ�`�F�b�N�|�C���g�ɐڐG�����玟�̃`�F�b�N�|�C���g���w��
    }

    //���G��Ă���CheckPoint���擾����
    private void OnTriggerEnter(Collider other)
    {
        //�ڐG���肪NextPoint�Ȃ�
        if (other.transform == _nextPoint)
        {
            _nowPoint = _nextPoint;
            _nextPoint = _chedkPoint[_checkPointManage];
            _checkPointManage++;
            _checkPointManage %= _chedkPoint.Length;
        }
        //�ڐG���肪NowPoint�Ȃ�
        if (other.transform == _nowPoint)
        {

        }
    }
}