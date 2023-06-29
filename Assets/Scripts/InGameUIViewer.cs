using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class InGameUIViewer : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _scoreDisplayTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timeBonusSlider;
    
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private BarrageManager _barrageManager;

    private const int MAX_BARRAGE_COUNT = 100;
    private void Awake()
    {
        SetEvent();
    }

    private void SetEvent()
    {
        ScoreManager.CurrentClearRecord.ObserveReplace()
            .Subscribe(record => DisplayScoreText(record.Key, record.NewValue));

        _timeManager.CurrentTime.Subscribe(DisplayTime);
        _barrageManager.BarrageCount.Subscribe(DisplayBarrageSlider);
    }

    private void DisplayScoreText(int level,int score)
    {
        _scoreDisplayTextList[level].text = score.ToString("00");
    }

    private void DisplayTime(float time)
    {
        _timerText.text = "残り<size=+20>" + time.ToString("000") + "</size>秒";
    }

    private void DisplayBarrageSlider(int barrageNum)
    {
        _timeBonusSlider.value = (float)barrageNum / MAX_BARRAGE_COUNT;
    }
}
