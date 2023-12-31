using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Dictionary<int, int> _scoreByLevelTable = new Dictionary<int, int>()
    {
        { 0, 100 },
        { 1, 180 },
        { 2, 240 },
        { 3, 380 },
        { 4, 500 }
    };

    private ReactiveDictionary<int, int> _currentClearRecord = new ReactiveDictionary<int, int>()
    {
        { 0, 0 },
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 }
    };

    public IReadOnlyReactiveDictionary<int, int> CurrentClearRecord => _currentClearRecord;

    public void SaveScore()
    {
        _currentClearRecord.ToList().ForEach(record => 
            PlayerPrefs.SetInt("score"+record.Key.ToString(),record.Value));
        PlayerPrefs.SetInt("total",GetTotalScore());
        PlayerPrefs.Save();
    }

    public void EatSushi(int eatSushiLevel)
    {
        _currentClearRecord[eatSushiLevel] += 1;
    }

    public int GetTotalScore()
    {
        var totalScore = 0;
        _scoreByLevelTable.ToList().ForEach(table => totalScore += _currentClearRecord[table.Key] * table.Value);
        return totalScore;
    }
}
