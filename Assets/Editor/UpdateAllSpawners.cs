using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Util;

public class UpdateAllSpawners : MonoBehaviour
{

    [MenuItem("Assets/Update Spawners")]
    static void UpdateSpawners()
    {
        SpawnStuff[] stuff = FindObjectsOfType<SpawnStuff>();
        foreach (SpawnStuff s in stuff)
        {
            s.SpawnItems();
        }
    }
}
