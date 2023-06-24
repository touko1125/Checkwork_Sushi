using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const float DEF_PLAY_TIME = 60.0f;
    private ReactiveProperty<float> _currentTimer = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> CurrentTime => _currentTimer;

    public Action OnEndTimer;
    private bool isCountTime = false;

    private void Awake()
    {
        _currentTimer.Value = DEF_PLAY_TIME;
        isCountTime = true;
        
        this.UpdateAsObservable()
            .Where(_ => isCountTime)
            .Subscribe(_ => CheckStopTimer())
            .AddTo(this);
        
        this.UpdateAsObservable()
            .Where(_ => isCountTime)
            .Subscribe(_ => CountTimer())
            .AddTo(this);
    }

    private void CheckStopTimer()
    {
        if (_currentTimer.Value > 0) return;
        OnEndTimer?.Invoke();
        isCountTime = false;
    }

    private void CountTimer()
    {
        _currentTimer.Value -= Time.deltaTime;
    }

    public void PlusTime(float time)
    {
        _currentTimer.Value += time;
    }
}
