using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

public class BarrageManager : MonoBehaviour
{
    private int _barrageCount = 0;
    public int BarrageCount => _barrageCount;

    [SerializeField] private TimeManager _timeManager;

}
