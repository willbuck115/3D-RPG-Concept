using System.IO;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadDialogue
{

    public static string[] GetDialogue(TextAsset textFile)
    {
        string file = textFile.text;
        var lines = file.Split("\n"[0]);
        return lines;
    }
}
