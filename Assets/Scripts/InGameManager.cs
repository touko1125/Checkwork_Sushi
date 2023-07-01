using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private SushiController _sushiController;

    [SerializeField] private ThemeManager _themeManager;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private BarrageManager _barrageManager;

    private Dictionary<int, int> clearThemeLevelDict = new Dictionary<int, int>()
    {
        { 0, 5 },
        { 1, 10 },
        { 2, 17 },
        { 3, 25 },
        { 4, 35 }
    };

    private int _currentThemeLevel = 0;
    private int _currentClearThemeNum = 0;
    private Theme _currentTheme;
    private string _currentInputStr = "";

    private async void Awake()
    {
        SetEvent();
        
        await _themeManager.LoadThemeData();
        InitTheme();
    }
    
    private void SetEvent()
    {
        _inputManager.OnInputAnyKey = ReceiveInputKey;
        _sushiController.OnReachedDestination = EndGame;
        _timeManager.OnEndTimer = EndGame;
    }

    private bool IsMatchInputTheme()
    {
        var isAllMatch = _currentTheme.themeAlphabets.FindAll(alphabet =>
            alphabet.StartsWith(_currentInputStr) && alphabet.Length == _currentInputStr.Length).Count > 0;

        return isAllMatch;
    }

    private void ReceiveInputKey(char inputChar)
    {
        //NULL文字であれば処理をしない
        if (inputChar == '\0') return;

        //新規に入力があった文字を足し合わせて正答アルファベット例の中のいずれかと前方一致するかどうかを判定
        var checkInputStr = _currentInputStr + inputChar;
        var matchedAlphabetList = _currentTheme.themeAlphabets.Where(alphabet => alphabet.StartsWith(checkInputStr)).ToList();

        var isMatchChar = matchedAlphabetList.Count > 0;

        if (!isMatchChar)
        {
            //連打数のリセット
            _barrageManager.ResetBarrageCount();
        }
        else
        {
            //連打数の加算
            _barrageManager.PlusBarrageCount();
            
            //一致すれば文字列を更新
            _currentInputStr = checkInputStr;
            _themeManager.HighlightThemeAlphabet(_currentInputStr,matchedAlphabetList[0]);
            
            //文字列全体がお題に一致しているかどうかを判定
            if (IsMatchInputTheme()) ClearCurrentTheme();   
        }
    }

    private void UpdateThemeLevel()
    {
        _currentClearThemeNum++;
        if (_currentClearThemeNum > clearThemeLevelDict[_currentThemeLevel])
        {
            _currentThemeLevel++;
            var maxLevel = clearThemeLevelDict.Keys.ToList()[clearThemeLevelDict.Count - 1];
            if(_currentThemeLevel == maxLevel)
            {
                _currentThemeLevel = 0;
                _currentClearThemeNum = 0;
            }
        }
    }

    private void ClearCurrentTheme()
    {
        _scoreManager.EatSushi(_currentThemeLevel);
        UpdateThemeLevel();
        InitTheme();
    }

    private void EndGame()
    {
        _scoreManager.SaveScore();
        SceneManager.LoadScene("Result");
    }

    private void InitTheme()
    {
        _currentInputStr = "";
        _currentTheme = _themeManager.GenerateTheme(_currentThemeLevel);
        _sushiController.Init(_currentThemeLevel);
    }
}
