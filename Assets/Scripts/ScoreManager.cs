using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance{ 
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
            }
            return instance;
        }
    }
    
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

    private void Awake()
    {
        CheckInstance();
    }

    private void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitScore()
    {
        _currentClearRecord.Values.ToList().ForEach(record => record = 0);
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
