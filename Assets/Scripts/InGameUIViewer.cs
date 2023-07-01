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
    
    [SerializeField] private Image _themeBackImage;
    [SerializeField] private TextMeshProUGUI _themeJapaneseText;
    [SerializeField] private TextMeshProUGUI _themeAlphabetText;
    
    [SerializeField] private TimeManager _timeManager;

    private const int MAX_BARRAGE_COUNT = 100;
    private void Awake()
    {
        SetEvent();
    }

    private void SetEvent()
    {
        _timeManager.CurrentTime.Subscribe(DisplayTime);
    }

    private void DisplayScoreText(int level,int score)
    {
        _scoreDisplayTextList[level].text = score.ToString("00");
    }

    private void DisplayThemeText(string japanese,string alphabet)
    {
        _themeJapaneseText.text = japanese;
        _themeAlphabetText.text = alphabet;
    }

    private void DisplayBarrageSlider(int barrageNum)
    {
        _timeBonusSlider.value = (float)barrageNum / MAX_BARRAGE_COUNT;
    }
    
    private void DisplayTime(float time)
    {
        _timerText.text = "残り<size=+20>" + time.ToString("000") + "</size>秒";
    }
}
