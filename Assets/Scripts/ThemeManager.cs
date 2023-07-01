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

}
