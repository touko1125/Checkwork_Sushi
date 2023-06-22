using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    private Dictionary<int, int> _currentClearRecord = new Dictionary<int, int>()
    {
        { 0, 0 },
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 }
    };

    [SerializeField] private List<TextMeshProUGUI> _scoreDisplayTextList = new List<TextMeshProUGUI>();

    public void EatSushi(int eatSushiLevel)
    {
        _currentClearRecord[eatSushiLevel] += 1;
        _scoreDisplayTextList[eatSushiLevel].text = _currentClearRecord[eatSushiLevel].ToString("00");
    }

    public int GetTotalScore()
    {
        var totalScore = 0;
        _scoreByLevelTable.ToList().ForEach(table => totalScore += _currentClearRecord[table.Key] * table.Value);
        return totalScore;
    }
}
