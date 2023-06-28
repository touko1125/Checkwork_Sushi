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

    private int _currentThemeLevel = 0;
    private Theme _currentTheme;
    private string _currentInputStr = "";

    private void Awake()
    {
        InitTheme();
        _scoreManager.InitScore();

        _inputManager.OnInputAnyKey = ReceiveInputKey;
        _sushiController.OnReachedDestination = OutCurrentTheme;
        _timeManager.OnEndTimer = TimeUp;
    }

    private bool IsMatchInputTheme()
    {
        var currentInputCharNum = _currentInputStr.Length;

        var inputMatchTheme = new List<string>(_currentTheme.themeAlphabets);
        //現在の入力全ての文字に対して文字が一致するかの判定
        for (var i = 0; i < currentInputCharNum; i++)
        {
            //解答例の中で文字がカーソル一の文字と一致しないものを削除していく
            inputMatchTheme.RemoveAll(alphabet => alphabet[i] != _currentInputStr[i]);
            
            //一致するテーマ文字がなくなった時点で一致するものはない
            if (inputMatchTheme.Count == 0) return false;
        }

        //この時点では入力文字列の中にマッチするものが1つ以上あるはずなので一番上のものを表示しハイライトする
        _themeManager.HighlightThemeAlphabet(_currentInputStr,inputMatchTheme[0]);

        //お題の文字数に入力文字数が達しているか
        var isReachedThemeCharNum =
            inputMatchTheme.FindAll(alphabet => alphabet.Length == currentInputCharNum).Count > 0;

        return isReachedThemeCharNum;
    }

    private void ReceiveInputKey(char inputChar)
    {
        //NULL文字であれば処理をしない
        if (inputChar == '\0') return;

        //新規にきた文字がお題に一致しているかどうか判定
        var isMatchChar = _currentTheme.themeAlphabets
            .FindAll(alphabet => alphabet.Length > _currentInputStr.Length &&
                                 alphabet[_currentInputStr.Length] == inputChar).Count > 0;

        if (!isMatchChar)
        {
            //連打数のリセット
            _barrageManager.ResetBarrageCount();
        }
        else
        {
            //連打数の加算
            _barrageManager.PlusBarrageCount();
            
            //一致すれば文字列に追加
            _currentInputStr += inputChar;
            
            //文字列全体がお題に一致しているかどうかを判定
            if (IsMatchInputTheme()) ClearCurrentTheme();   
        }
    }

    private void ClearCurrentTheme()
    {
        _scoreManager.EatSushi(_currentThemeLevel);
        InitTheme();
    }

    private void TimeUp()
    {
        SceneManager.LoadScene("Result");
    }

    private void OutCurrentTheme()
    {
        SceneManager.LoadScene("Result");
    }

    private void InitTheme()
    {
        _currentInputStr = "";
        _currentTheme = _themeManager.GenerateTheme(_currentThemeLevel);
        _sushiController.Init(_currentThemeLevel);
    }
}
