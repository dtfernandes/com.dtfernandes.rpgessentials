using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class StringExtensions
{
    internal static string CapFirst(this string String)
    {
        return String[0].ToString().ToUpper() + 
                String.Substring(1, String.Length - 1);
    }

}
