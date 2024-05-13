using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class DungeonData : ScriptableObject
{
    public List<Vector3> spawnPoints = new List<Vector3>();
}
