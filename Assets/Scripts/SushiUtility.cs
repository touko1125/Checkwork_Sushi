using System;

public static class SushiUtility
{
    public static T ConvertIntToEnum<T>(this int num)
    {
        return (T)Enum.ToObject(typeof(T), num);
    }
}