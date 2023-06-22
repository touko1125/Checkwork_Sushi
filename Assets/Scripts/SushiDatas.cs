using System;
using UnityEngine;

public enum PlateColor
{
    Orange,
    Green,
    Red,
    Gray,
    Gold
}

public enum SushiType
{
    Ebi,
    Ikura,
    Maguro,
    Samon,
    Tako,
    Tamago
}

[Serializable]
public struct PlateData
{
    public PlateColor color;
    public Sprite sprite;
}

[Serializable]
public struct SushiData
{
    public SushiType type;
    public Sprite sprite;
}
