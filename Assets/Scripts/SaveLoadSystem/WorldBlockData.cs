using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldBlockSaveData
{
    public List<WorldBlock> Blocks;
}

[Serializable]
public class WorldBlock
{
    public long BlockId;
    public Vector3 Position;
}