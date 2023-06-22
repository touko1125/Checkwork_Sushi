using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class SushiController : MonoBehaviour
{
    public Action OnReachedDestination;
    private Tween _moveTween;
    private const float MOVE_TIME = 10.0f;

    [SerializeField] private RectTransform _startPos;
    [SerializeField] private RectTransform _destinationPos;

    [SerializeField] private Image _plateImage;
    [SerializeField] private Image _sushiImage;
    
    [SerializeField] private List<PlateData> _plateDatas;
    [SerializeField] private List<SushiData> _sushiDatas;

    public void Init(int currentLevel)
    {
        //現在の移動のキャンセル
        _moveTween.Kill();
        
        //寿司の位置の変更
        this.GetComponent<RectTransform>().anchoredPosition = _startPos.anchoredPosition;
        
        //寿司の見た目の変更
        _sushiImage.sprite = _sushiDatas[Random.Range(0, _sushiDatas.Count)].sprite;
        _plateImage.sprite = _plateDatas[currentLevel].sprite;

        _moveTween = this.GetComponent<RectTransform>()
            .DOAnchorPos(_destinationPos.anchoredPosition, MOVE_TIME)
            .SetEase(Ease.Linear)
            .OnComplete(OnCompleteMove)
            .SetLink(this.gameObject);
    }

    private void OnCompleteMove()
    {
        OnReachedDestination?.Invoke();
    }
}
