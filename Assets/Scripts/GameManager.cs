using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SushiController _sushiController;

    [SerializeField] private ThemeManager _themeManager;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private ScoreManager _scoreManager;

    private int _currentThemeLevel = 0;
    private Theme _currentTheme;
    private string _currentInputStr = "";

    private void Awake()
    {
        InitTheme();

        _inputManager.OnInputAnyKey = input => ReceiveInputKey(input);
        _sushiController.OnReachedDestination = OutCurrentTheme;
    }

    private bool IsMatchInputAndTheme()
    {
        var currentInputCharNum = _currentInputStr.Length;

        var inputMatchTheme = _currentTheme.themeAlphabets;
        //現在の入力全ての文字に対して文字が一致するかの判定
        for (var i = 0; i < currentInputCharNum; i++)
        {
            //一致するテーマ文字がなくなった時点で一致するものはない
            if (inputMatchTheme.Count == 0) return false;
            
            //解答例の中で文字がカーソル一の文字と一致しないものを削除していく
            inputMatchTheme.RemoveAll(alphabet => alphabet[i] != _currentInputStr[i]);
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
        
        if (inputChar == '\b')
        {
            if (_currentInputStr.Length == 0) return;
            
            //最後の文字を取り除く
            _currentInputStr.Remove(_currentInputStr.Length - 1);
        }
        else
        {
            _currentInputStr += inputChar;
        }

        //入力を受け取って文字の更新が終わったタイミングでテーマを一致しているかの判定をする
        if (IsMatchInputAndTheme()) ClearCurrentTheme();
    }

    private void ClearCurrentTheme()
    {
        Debug.Log("clear current theme");
        _scoreManager.EatSushi(_currentThemeLevel);
        InitTheme();
    }

    private void OutCurrentTheme()
    {
        Debug.Log("out current theme");
    }

    private void InitTheme()
    {
        _currentInputStr = "";
        _currentTheme = _themeManager.GenerateTheme(_currentThemeLevel);
        _sushiController.Init(_currentThemeLevel);
    }
}
