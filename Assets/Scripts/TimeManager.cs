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
    private float _currentTimer;
    
    private const float MAX_BARRAGE_COUNT = 100;
    private Dictionary<int, float> _barrageTimeBonusDict = new Dictionary<int, float>()
    {
        { 25, 1 },
        { 50, 1 },
        { 75, 2 },
        { 100, 3 }
    };

    public Action OnEndTimer;
    private bool isCountTime = false;
    
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timeBonusSlider;

    private void Awake()
    {
        _currentTimer = DEF_PLAY_TIME;
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
        if (_currentTimer > 0) return;
        OnEndTimer?.Invoke();
        isCountTime = false;
    }

    private void CountTimer()
    {
        _currentTimer -= Time.deltaTime;
        _timerText.text = "残り<size=+20>" + _currentTimer.ToString("000") + "</size>秒";
    }

    public void CalculateTimeBonus(int barrageCount)
    {
        _timeBonusSlider.value = (float)barrageCount / MAX_BARRAGE_COUNT;
        var barrageBonus = _barrageTimeBonusDict.ToList().Find(pair => barrageCount == pair.Key);
        if (barrageBonus.Equals(default(KeyValuePair<int, float>))) _currentTimer += barrageBonus.Value;
    }
}
