using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Theme
{
    //お題の日本語ひらがなの文字数
    public int themeCharNum => themeStr.Length;

    //お題の日本語ひらがなの文字数に応じたお題レベル
    public int themeLevel
    {
        get
        {
            if (themeCharNum < 4) return 0;
            else if (themeCharNum < 6) return 1;
            else if (themeCharNum < 8) return 2;
            else if (themeCharNum < 10) return 3;
            else return 4;
        }
    }

    public string themeStr;     //お題日本語ひらがな文字列
    public string themeStrForDisplay;       //お題の表示用の日本語文字列
    public List<string> themeAlphabets;     //お題の正答判定用のアルファベット文字列
}

[CreateAssetMenu(menuName = "ScriptableObject/Theme",fileName = "ThemeData")]
public class ThemeData : ScriptableObject
{
    public List<Theme> themeList = new List<Theme>();
}
