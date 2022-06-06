using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageSelectController : MonoBehaviour
{
    [Header("�X�e�[�W��")]
    [SerializeField, Tooltip("�V�[����")] string _stageName;

    [Header("AlphaImage")]
    [SerializeField, Tooltip("AlphaImage")] Image _alphaImage;
    public void OnClickStageSelect()
    {
        SceneManager.LoadScene(_stageName);
        DOTween.ToAlpha(
                () => _alphaImage.color,
                color => _alphaImage.color = color,
                1f, // �ڕW�l
                3f // ���v����
                ).OnComplete(() =>
                {
                    SceneManager.LoadScene(_stageName);
                });
    }
}
