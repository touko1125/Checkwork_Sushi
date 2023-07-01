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
    [SerializeField] private TimeManager _timeManager;

    private int _currentThemeLevel = 1;

    private async void Awake()
    {
        SetEvent();
        await _themeManager.LoadThemeData();
        InitTheme();
    }
    
    private void SetEvent()
    {
        _sushiController.OnReachedDestination = EndGame;
        _timeManager.OnEndTimer = EndGame;
    }
    
    private void ClearTheme()
    {
        InitTheme();
    }

    private void EndGame()
    {
        SceneManager.LoadScene("Result");
    }

    private void InitTheme()
    {
        _sushiController.Init(_currentThemeLevel);
    }
}
