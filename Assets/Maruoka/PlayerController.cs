using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("L・Rshiftで判定をとる")] string _shiftName;

    [Tooltip("プレイヤーのRigidbody")]
    Rigidbody _rb;

    [Tooltip("プレイヤーのトランスフォーム")]
    Transform _transform;

    [Tooltip("プレイヤーに加える力")]
    [SerializeField] Vector3 _playerForcePower;
    [Tooltip("前進キーを離している間の減速率")]
    [SerializeField] Vector3 _playerDeceleratePower;

    [Tooltip("チェックポイントの配列")]
    [SerializeField] Transform[] _chedkPoint;

    [Tooltip("現在の速度")]
    public float _nowSpeed1;
    [Tooltip("現在の速度")]
    public float _nowSpeed2;

    Transform _nowPoint = default;
    Transform _nextPoint = default;

    [SerializeField,Tooltip("スタートしたかの判定")] SignalCheck _signalCheck;
    /// <summary>次のチェックポイントのインデックスを指す</summary>
    int _checkPointManage = 1;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        //nullチェック
        if (_rb == null)
        {
            Debug.Log("プレイヤーのRigitbodyがアサインされてません");
        }
        if (_transform == null)
        {
            Debug.Log("プレイヤー2のTransformがアサインされてません");
        }
        if ((_nextPoint = _chedkPoint[0]) == null)
        {
            Debug.Log("チェックポイントがアサインされていません");
        }
        _nowPoint = _chedkPoint[0];
    }


    void Update()
    {
        if (!_signalCheck._sCheck) return;

        //移動する
        PlayerMoveManager(_transform, _rb);
        //表示用速度を取得する
        _nowSpeed1 = Mathf.Sqrt((_rb.velocity.x * _rb.velocity.x) + (_rb.velocity.z * _rb.velocity.z));
    }

    void PlayerMoveManager(Transform tr, Rigidbody rb)
    {
        //任意のボタンが押されている間、_playerForcePowerを加える
        if (Input.GetButton(_shiftName))
        {
            MoveCheckPoint(tr, rb, _nextPoint.GetChild(0));
        }
        //離している間の処理
        //else
        //{
        //    //プレイヤーからチェックポイントまでの方向ベクトルを取得する
        //    Vector3 directionVector = _nextPoint.GetChild(0).position - _transform.position;

        //    //方向ベクトルをノーマライズ化
        //    directionVector = directionVector.normalized;

        //    if (Input.GetButton("Jump"))
        //    {
        //        Debug.Log(directionVector);
        //    }
        //    rb.velocity = new Vector3(rb.velocity.x * directionVector.x * 1f, rb.velocity.y, rb.velocity.z * directionVector.z * 1f);
        //}

        //離している間の処理
        //else
        //{
        //    //速度がほとんどでていなければ何もしない
        //    if ((Mathf.Abs(rb.velocity.x) < 0.3f || Mathf.Abs(rb.velocity.z) < 0.3f))
        //    {
        //        return;
        //    }
        //    //そうでなければ減速する
        //    else
        //    {
        //        SeparatedMove(tr, rb, _nextPoint.GetChild(0));
        //    }

        //}

        //旧コード:減速処理を実装しようとした

        //ここから
        ////離されている間は減速する
        //else if (rb.velocity.z > 0 || rb.velocity.x > 0)//前進力が残っていたら減速
        //{
        //    //rb.AddForce(rb.velocity.x, rb.velocity.y, _playerDeceleratePower.z);
        //}
        //else if ((rb.velocity.z <= 0) || (rb.velocity.x <= 0))//後進はしない
        //{
        //    //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        //}
        //ここまで
    }


    void MoveCheckPoint(Transform pt, Rigidbody rb, Transform nextPosition)
    {
        //プレイヤーからチェックポイントまでの方向ベクトルを取得する
        Vector3 directionVector = nextPosition.position - pt.position;

        //方向ベクトルをノーマライズ化
        directionVector = directionVector.normalized;


        //力の向きと強さを掛ける
        Vector3 newForce = default;
        newForce.x = directionVector.x * _playerForcePower.x;
        newForce.z = directionVector.z * _playerForcePower.z;

        rb.AddForce(newForce.x, rb.velocity.y, newForce.z);
    }

    //離している間の移動処理
    void SeparatedMove(Transform pt, Rigidbody rb, Transform nextPosition)
    {
        //プレイヤーからチェックポイントまでの方向ベクトルを取得する
        Vector3 directionVector = nextPosition.position - pt.position;

        //方向ベクトルをノーマライズ化
        directionVector = directionVector.normalized;


        //力の向きと減速率を掛ける
        Vector3 newForce = default;
        //newForce.x = directionVector.x * _playerDeceleratePower.x;
        //newForce.z = directionVector.z * _playerDeceleratePower.z;
        rb.velocity -= _playerDeceleratePower;

        rb.AddForce(newForce.x, rb.velocity.y, newForce.z);
    }

    //今触れているCheckPointを取得する
    private void OnTriggerEnter(Collider other)
    {
        //接触相手がNextPointなら
        if (other.transform == _nextPoint)
        {
            _nowPoint = _nextPoint;
            _nextPoint = _chedkPoint[_checkPointManage];
            _checkPointManage++;
            _checkPointManage %= _chedkPoint.Length;
        }
        //接触相手がNowPointなら
        if (other.transform == _nowPoint)
        {

        }
    }
}
