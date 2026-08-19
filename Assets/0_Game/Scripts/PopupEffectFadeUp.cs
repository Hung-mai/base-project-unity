using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupEffectFadeUp : MonoBehaviour
{
    public float targetScale = 1;
    public float timeFX = 0.5f;
    public Transform _transform;
    public CanvasGroup _canvasGroup;
    public float startY = 0;
    
    void OnEnable()
    {
        _transform.localPosition = new Vector2(0, startY-50);
        _transform.DOLocalMoveY(startY, timeFX);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, timeFX);
    }
}
