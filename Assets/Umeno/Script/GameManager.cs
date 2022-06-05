using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text _GoalCount1;//Player1���S�[�������񐔂�\������e�L�X�g
    [SerializeField] Text _GoalCount2;//Player2���S�[�������񐔂�\������e�L�X�g
    [SerializeField] Text _Base;//Winner�\���e�L�X�g�̃x�[�X
    [SerializeField] GameObject _button;//Button��Active�ɂ���ϐ�
    [SerializeField] Image _alphaImage;
    [SerializeField] ParticleSystem _Winner;//Winner�\���̎���Particle
    [SerializeField] AudioSource _WinnerSound;//Winner�\���̎���Sound
    [SerializeField] Text _result;//Winner�\���̎���RESULT��\��
    AudioSource _goalsound;//Goal����Sound

    //�c��̎���
    int _count1 = 3;
    int _count2 = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    /// <summary>
    /// ���߂ɌĂ΂�鏈��
    /// </summary>
    private void Start()
    {
        DOTween.ToAlpha(() => _alphaImage.color, color => _alphaImage.color = color,
            0f,
            3f
        );
        _goalsound = GetComponent<AudioSource>();
        _WinnerSound = _WinnerSound.GetComponent<AudioSource>();

        _GoalCount1.text = $"�c��{_count1}��";
        _GoalCount2.text = $"�c��{_count2}��";
    }
    /// <summary>
    /// Player1�E2�̃S�[���J�E���g
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            _count1--;
            _goalsound.Play();
            _GoalCount1.text = $"�c��{_count1}��";
            GoalJudge();
        }
        if (other.gameObject.CompareTag("Player2"))
        {
            _count2--;
            _goalsound.Play();
            _GoalCount2.text = $"�c��{_count2}��";
            GoalJudge();
        }
    }

    /// <summary>
    /// �S�[������
    /// </summary>
    /// <param name="GoalCount"></param>
    void GoalJudge()
    {

        if (0 >= _count1)
        {
            _Base.text = "Player1 Win";
            _button.gameObject.SetActive(true);
            _Winner.gameObject.SetActive(true);
            _result.gameObject.SetActive(true);
            _WinnerSound.Play();
            _Base.color = Color.red;
        }
        if (0 >= _count2)
        {
            _Base.text = "Player2 Win";
            _button.gameObject.SetActive(true);
            _Winner.gameObject.SetActive(true);
            _result.gameObject.SetActive(true);
            _WinnerSound.Play();
            _Base.color = Color.blue;
        }
    }
}
