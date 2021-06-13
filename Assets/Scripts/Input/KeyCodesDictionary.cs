
using System.Collections.Generic;
using UnityEngine;

public static class KeyCodesDictionary
{
    private static readonly Dictionary<KeyCode, string> dictionary = new Dictionary<KeyCode, string>()
    {
        {KeyCode.A, "A" },
        {KeyCode.B, "B" },
        {KeyCode.C, "C" },
        {KeyCode.D, "D" },
        {KeyCode.E, "E" },
        {KeyCode.F, "F" },
        {KeyCode.G, "G" },
        {KeyCode.H, "H" },
        {KeyCode.I, "I" },
        {KeyCode.J, "J" },
        {KeyCode.K, "K" },
        {KeyCode.L, "L" },
        {KeyCode.M, "M" },
        {KeyCode.N, "N" },
        {KeyCode.O, "O" },
        {KeyCode.P, "P" },
        {KeyCode.Q, "Q" },
        {KeyCode.R, "R" },
        {KeyCode.S, "S" },
        {KeyCode.T, "T" },
        {KeyCode.U, "U" },
        {KeyCode.V, "V" },
        {KeyCode.W, "W" },
        {KeyCode.X, "X" },
        {KeyCode.Y, "Y" },
        {KeyCode.Z, "Z" },
    };

    public static string GetNameForAction(Actions action)
    {
        KeyCode code = InputManager.GetKeyCodeForAction(action);
        if (dictionary.ContainsKey(code))
            return dictionary[code];
        return code.ToString();
    }

}