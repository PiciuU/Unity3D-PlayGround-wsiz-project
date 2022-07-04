using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

    public static bool IntToBool(int value)
    {
        return (value == 1);
    }

}
