using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugExtentions
{
    public static void print<T>(this IEnumerable<T> self)
    {
        string printString = "";
        foreach(T t in self)
        {
            printString += t.ToString() + "\n" ;
        }

        if (printString == "")
            Debug.Log("Null");
        else
            Debug.Log(printString);
    }
}
