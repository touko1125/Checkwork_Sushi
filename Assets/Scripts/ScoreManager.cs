using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static Dictionary<int, int> _scoreByLevelTable = new Dictionary<int, int>()
    {
        { 0, 100 },
        { 1, 180 },
        { 2, 240 },
        { 3, 380 },
        { 4, 500 }
    };

    private static ReactiveDictionary<int, int> _currentClearRecord = new ReactiveDictionary<int, int>()
    {
        { 0, 0 },
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 }
    };

    public static IReadOnlyReactiveDictionary<int, int> CurrentClearRecord => _currentClearRecord;

    public void InitScore()
    {
        _currentClearRecord.Values.ToList().ForEach(record => record = 0);
    }

    public void EatSushi(int eatSushiLevel)
    {
        _currentClearRecord[eatSushiLevel] += 1;
    }

    public static int GetTotalScore()
    {
        var totalScore = 0;
        _scoreByLevelTable.ToList().ForEach(table => totalScore += _currentClearRecord[table.Key] * table.Value);
        return totalScore;
    }
}
