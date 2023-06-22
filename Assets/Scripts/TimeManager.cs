using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const float DEF_PLAY_TIME = 60.0f;
    private float _currentTimer;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timeBonusSlider;

    private void Awake()
    {
        _currentTimer = DEF_PLAY_TIME;
        this.UpdateAsObservable().Subscribe(_ => CountTimer());
    }

    private void CountTimer()
    {
        _currentTimer -= Time.deltaTime;
        _timerText.text = "残り<size=+20>" + _currentTimer.ToString("000") + "</size>秒";
    }
}
