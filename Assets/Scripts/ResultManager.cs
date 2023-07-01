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
    
    
    public void RetryButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
