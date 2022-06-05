using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneController : MonoBehaviour
{
    [Header("Player1")]
    [SerializeField] GameObject _leftTextObj;
    Text _leftText;
    Animator _leftAnim;
    [SerializeField] bool _readyCeack1;
    [Header("Player2")]
    [SerializeField] GameObject _rightTextObj;
    Text _rightText;
    Animator _rightAnim;
    [SerializeField] bool _readyCeack2;

    [Header("���̑�")]
    [SerializeField] Image _alphaImage;


    // Start is called before the first frame update
    void Start()
    {
        _leftText = _leftTextObj.gameObject.GetComponent<Text>();
        _leftAnim = _leftText.gameObject.GetComponent<Animator>();
        _rightText = _rightTextObj.gameObject.GetComponent<Text>();
        _rightAnim = _rightText.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputShift();


        if (_readyCeack1 && _readyCeack2) //��Ƃ�True�ɂȂ�����V�[���ړ�
        {
            DOTween.ToAlpha(
                () => _alphaImage.color,
                color => _alphaImage.color = color,
                1f, // �ڕW�l
                3f // ���v����
                ).OnComplete(() =>
                {
                    SceneManager.LoadScene("SampleScene");
                });    
        }
    }

    /// <summary>
    /// R�ELShift�̃L�[���͂̔���
    /// </summary>
    private void InputShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _leftText.text = "OK!!";
            _leftText.fontSize = 150;
            _readyCeack1 = true;
            _leftAnim.SetBool("Ready", true);
        }
        if (Input.GetKey(KeyCode.RightShift))
        {
            _rightText.text = "OK!!";
            _rightText.fontSize = 150;
            _readyCeack2 = true;
            _rightAnim.SetBool("Ready", true);
        }
    }
}
