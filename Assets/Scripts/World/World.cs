using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour, IWorld
{
    /// <summary> All the blocks that are placed in the world</summary>
    private List<Block> AllBlocks = new();

    private ISaveLoadSystem    _saveLoadSystem;
    private IBlockPool         _blockPool;
    private WorldBlockSaveData _worldBlockSaveData;
    
    // Start is called before the first frame update
    void Awake()
    {
        _saveLoadSystem = ServiceLocator.Get<ISaveLoadSystem>();
        _blockPool      = ServiceLocator.Get<IBlockPool>();
        
        ServiceLocator.Register<IWorld>(this);
    }

    private void Start()
    {
        LoadBlocks();
    }

    /// <summary>Load all blocks from save file</summary>
    private void LoadBlocks()
    {
        // Try to load the inventory data from the save file.
        // If there's no save file, then create new save data.
        _worldBlockSaveData = _saveLoadSystem.LoadData<WorldBlockSaveData>();
        if (_worldBlockSaveData == null)
        {
            _worldBlockSaveData = new WorldBlockSaveData();
            _worldBlockSaveData.Blocks = new();
        }
        
        // place those blocks in the world
        foreach (var block in _worldBlockSaveData.Blocks)
        {
            var blockObject = _blockPool.GetBlock(block.BlockId);
            blockObject.transform.position = block.Position;
            blockObject.GetComponent<BoxCollider>().enabled = true;
            AllBlocks.Add(blockObject);
        }
    }
    
    
    public void AddBlock(Block block)
    {
        AllBlocks.Add(block);
        _worldBlockSaveData.Blocks.Add(new WorldBlock()
        {
            BlockId  = block.Id,
            Position = block.transform.position,
        });
    }

    public void RemoveBlock(Block block)
    {
        AllBlocks.Remove(block);
        _worldBlockSaveData.Blocks.RemoveAll(b => b.BlockId == block.Id);
    }
    
    private void OnDestroy()
    {
        _saveLoadSystem.SaveData(_worldBlockSaveData);
    }
}
