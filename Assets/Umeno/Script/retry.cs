using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class retry : MonoBehaviour
{
    [SerializeField, Tooltip("AlphaImage")] Image _alphaImage;
    public void OnClickBackTitle()
    {
        DOTween.ToAlpha(() => _alphaImage.color, color => _alphaImage.color = color,
            1f,
            3f
        ).OnComplete(() => SceneManager.LoadScene("TitleScene")
        );
    }
}
