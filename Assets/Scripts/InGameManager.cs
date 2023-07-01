using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private SushiController _sushiController;

    [SerializeField] private ThemeManager _themeManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private BarrageManager _barrageManager;
    
    //各レベルに上がるためのお題の正答数
    private Dictionary<int, int> clearThemeLevelDict = new Dictionary<int, int>()
    {
        { 0, 5 },
        { 1, 10 },
        { 2, 17 },
        { 3, 25 },
        { 4, 35 }
    };

    private int _currentThemeLevel;     //現状のお題のレベル
    private int _currentClearThemeNum;  //現状の正答お題数
    
    private Theme _currentTheme;

    private async void Awake()
    {
        //ゲームクリア、ゲームオーバーのイベント設定
        SetEvent();
        
        //テーマのデータの読み込みを行ない、それが完了し次第最初のお題を出す
        await _themeManager.LoadThemeData();
        InitTheme();
    }

    private void SetEvent()
    {
        //ゲームオーバーの機能
        //Actionと呼ばれるイベント通知の機能を使用しています
        _sushiController.OnReachedDestination = EndGame;
        _timeManager.OnEndTimer = EndGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReceiveInput('a');
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ReceiveInput('b');
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ReceiveInput('c');
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ReceiveInput('d');
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ReceiveInput('e');
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ReceiveInput('f');
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ReceiveInput('g');
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            ReceiveInput('h');
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ReceiveInput('i');
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            ReceiveInput('j');
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ReceiveInput('k');
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ReceiveInput('l');
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            ReceiveInput('m');
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            ReceiveInput('n');
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ReceiveInput('o');
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ReceiveInput('p');
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ReceiveInput('q');
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ReceiveInput('r');
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ReceiveInput('s');
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            ReceiveInput('t');
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            ReceiveInput('u');
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            ReceiveInput('v');
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ReceiveInput('w');
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ReceiveInput('x');
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ReceiveInput('y');
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ReceiveInput('z');
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            ReceiveInput('-');
        }
    }

    //キー入力があった際に呼び出される関数
    //この中で現在のお題と入力文字列が一致するかどうか判定しよう！
    private void ReceiveInput(char inputChar)
    {
        
    }

    //判定の結果お題の文字が入力できていればこの関数を呼び出そう！
    private void ClearCurrentTheme()
    {
        //スコアの加算
        _scoreManager.EatSushi(_currentThemeLevel);
        
        //お題の正答数を増やし、必要に応じてお題のレベルを上げる
        UpdateThemeLevel();
        
        //お題の初期化
        InitTheme();
    }
    
    //これまでのお題の正答数に応じて問題のレベルを上げる関数
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
    
    //お題の新規作成を行う関数
    private void InitTheme()
    {
        //現在の問題レベルで新たな問題の作成
        _currentTheme = _themeManager.GenerateTheme(_currentThemeLevel);
        
        //寿司の見た目を現在のレベルの皿で初期化
        _sushiController.Init(_currentThemeLevel);
    }

    //ゲーム終了条件を満たしたらリザルト画面へ遷移
    private void EndGame()
    {
        SceneManager.LoadScene("Result");
    }
}
