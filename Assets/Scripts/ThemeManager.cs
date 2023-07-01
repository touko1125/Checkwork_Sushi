using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] private Image _themeBackImage;
    [SerializeField] private TextMeshProUGUI _themeJapaneseText;
    [SerializeField] private TextMeshProUGUI _themeAlphabetText;

    private ThemeData _themeListData;

    /// <summary>
    /// お題のデータリストの非同期読み込み用の関数
    /// </summary>
    public async UniTask LoadThemeData()
    {
        var loadAsync = Addressables.LoadAssetAsync<ThemeData>("ThemeData");
        await loadAsync.Task;
        _themeListData = loadAsync.Result;
    }

    /// <summary>
    /// レベルに応じたお題を策定し返す関数
    /// </summary>
    /// <param name="currentLevel">現状のお題のレベル</param>
    /// <returns>ランダムに選択されたお題の情報</returns>
    public Theme GenerateTheme(int currentLevel)
    {
        //お題の中でも現状の問題レベルのもののみを抽出してリスト化
        var currentLevenThemeList = _themeListData.themeList.FindAll(theme => theme.themeLevel == currentLevel);
        var randomTheme = currentLevenThemeList[Random.Range(0, currentLevenThemeList.Count)];

        //お題文字の表示。アルファベットはおそらく一般的であろうもの順に並べているため一番上のものを表示
        _themeJapaneseText.text = randomTheme.themeStrForDisplay;
        _themeAlphabetText.text = randomTheme.themeAlphabets[0];

        return randomTheme;
    }
    
    /// <summary>
    /// お題の文字を赤くハイライトする
    /// </summary>
    /// <param name="inputStr">現在の入力文字</param>
    /// <param name="mostMatchedStr">お題の文字の中で現状最も入力に一致している文字</param>
    public void HighlightThemeAlphabet(string inputStr,string mostMatchedStr)
    {
        //お題の文字列から入力されている分だけ削除する
        mostMatchedStr = mostMatchedStr.Replace(inputStr, "");

        //入力文だけ赤く表示する
        _themeAlphabetText.text = "<color=red>" + inputStr + "</color>" + mostMatchedStr;
    }
}
