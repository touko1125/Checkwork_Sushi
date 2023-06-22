using System;
using System.Collections.Generic;

[Serializable]
public class Theme
{
    public int themeCharNum
    {
        get { return themeStr.Length; }
    }

    public int themeLevel
    {
        get
        {
            if (themeCharNum < 4) return 0;
            else if (themeCharNum < 6) return 1;
            else if (themeCharNum < 8) return 2;
            else if (themeCharNum < 10) return 3;
            else return 4;
        }
    }

    public string themeStr;
    public string themeStrForDisplay;
    public List<string> themeAlphabets;
}
