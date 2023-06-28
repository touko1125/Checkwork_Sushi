using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private TextMeshProUGUI _finalBenefitText;
    
    [SerializeField] private List<TextMeshProUGUI> _scoreDisplayTextList = new List<TextMeshProUGUI>();

    private const int NORMAL_COURCE_LATCH = 3000;

    private void Awake()
    {
        DisplayFinalScore();
    }

    private void DisplayFinalScore()
    {
        var scoreDict = ScoreManager.Instance.CurrentClearRecord;
        scoreDict.ToList().ForEach(record => DisplayScoreText(record.Key,record.Value));

        var totalScore = ScoreManager.Instance.GetTotalScore();
        _totalScoreText.text = totalScore.ToString() + "円分のお寿司をゲット！";
        var finalBenefit = totalScore - NORMAL_COURCE_LATCH;
        _finalBenefitText.text = "<size=+10>" + finalBenefit.ToString() + "</size> <color=#44933F>円分お得でした！</color>";
    }

    private void DisplayScoreText(int level,int score)
    {
        _scoreDisplayTextList[level].text = score.ToString("00");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
