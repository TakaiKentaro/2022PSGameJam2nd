using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectController : MonoBehaviour
{
    [Header("ステージ名")]
    [SerializeField, Tooltip("シーン名")] string _stageName;

    [Header("AlphaImage")]
    [SerializeField, Tooltip("AlphaImage")] Image _alphaImage;
    public void OnClickStageSelect()
    {
        SceneManager.LoadScene(_stageName);
        DOTween.ToAlpha(
                () => _alphaImage.color,
                color => _alphaImage.color = color,
                1f, // 目標値
                3f // 所要時間
                ).OnComplete(() =>
                {
                    SceneManager.LoadScene(_stageName);
                });
    }
}
