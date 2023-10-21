using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IBlockPool, IBootstrapLoader
{
    [SerializeField] private BlockData blockData;
    [SerializeField] private int       poolSize;
    
    private Dictionary<long, List<Block>> _blocks = new();

    
    public IEnumerator Load()
    {
        ServiceLocator.Register<IBlockPool>(this);
        foreach (var blockItemSo in blockData.Blocks)
        {
            // for each block we instantiate a list of blocks
            var blockList = new List<Block>();
            for (int i = 0; i < poolSize; i++)
            {
                // instantiate a block
                var block = Instantiate(blockItemSo.Block, transform);
                block.Id = blockItemSo.Id;
                block.gameObject.SetActive(false);
                blockList.Add(block);
            }
            
            // add the list of blocks to the dictionary
            _blocks.Add(blockItemSo.Id, blockList);
        }
        
        yield return null;
    }

    public Block GetBlock(long blockId)
    {
        if (_blocks.TryGetValue(blockId, out var blockList))
        {
            if (blockList.Count > 0)
            {
                var block = blockList[0];
                blockList.RemoveAt(0);
                block.gameObject.SetActive(true);
                return block;
            }
            
            // if the list is empty we instantiate a new block
            var blockItemSo = blockData.GetBlockItem(blockId);
            return Instantiate(blockItemSo.Block, transform);
        }

        return null;
    }

    public void ReturnBlock(Block block)
    {
        if (_blocks.TryGetValue(block.Id, out var blockList))
        {
            blockList.Add(block);
            block.gameObject.SetActive(false);
        }
    }
}
