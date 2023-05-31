using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "SpawnData", menuName = "SpawnData")]
[Serializable]
public class SpawnData : ScriptableObject
{
    public List<Vector3> Spawn = new();
}