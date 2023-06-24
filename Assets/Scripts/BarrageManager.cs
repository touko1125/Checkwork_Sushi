using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

public class BarrageManager : MonoBehaviour
{
    private Dictionary<int, float> _barrageTimeBonusDict = new Dictionary<int, float>()
    {
        { 25, 1 },
        { 50, 1 },
        { 75, 2 },
        { 100, 3 }
    };

    private ReactiveProperty<int> _barrageCount = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> BarrageCount => _barrageCount;

    [SerializeField] private TimeManager _timeManager;

    public void ResetBarrageCount()
    {
        _barrageCount.Value = 0;
    }
    
    public void PlusBarrageCount()
    {
        _barrageCount.Value++;
        var barrageBonus = _barrageTimeBonusDict.ToList().Find(pair => _barrageCount.Value == pair.Key);
        if (barrageBonus.Equals(default(KeyValuePair<int, float>)))
        {
            _timeManager.PlusTime(barrageBonus.Value);
        }
    }
}
