using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text _GoalCount1;//Player1がゴールした回数を表示するテキスト
    [SerializeField] Text _GoalCount2;//Player2がゴールした回数を表示するテキスト
    [SerializeField] Text _Base;//Winner表示テキストのベース
    [SerializeField] GameObject _button;//ButtonをActiveにする変数
    [SerializeField] Image _alphaImage;
    [SerializeField] ParticleSystem _Winner;//Winner表示の時のParticle
    [SerializeField] AudioSource _WinnerSound;//Winner表示の時のSound
    [SerializeField] Text _result;//Winner表示の時にRESULTを表示
    AudioSource _goalsound;//Goal時のSound

    //残りの周回数
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
    /// 初めに呼ばれる処理
    /// </summary>
    private void Start()
    {
        DOTween.ToAlpha(() => _alphaImage.color, color => _alphaImage.color = color,
            0f,
            3f
        );
        _goalsound = GetComponent<AudioSource>();
        _WinnerSound = _WinnerSound.GetComponent<AudioSource>();

        _GoalCount1.text = $"残り{_count1}周";
        _GoalCount2.text = $"残り{_count2}周";
    }
    /// <summary>
    /// Player1・2のゴールカウント
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            _count1--;
            _goalsound.Play();
            _GoalCount1.text = $"残り{_count1}周";
            GoalJudge();
        }
        if (other.gameObject.CompareTag("Player2"))
        {
            _count2--;
            _goalsound.Play();
            _GoalCount2.text = $"残り{_count2}周";
            GoalJudge();
        }
    }

    /// <summary>
    /// ゴール判定
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
