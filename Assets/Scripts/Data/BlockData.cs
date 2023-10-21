using System;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Block Data SO will be used to store all the block data.
    /// Each Block Item will has a unique id and a block object.
    /// And we can use this id to get the block object. Then spawn that block object in the scene.
    /// </summary>
    [CreateAssetMenu(fileName = "BlockDataSO", menuName = "Data/Block", order = 0)]
    public class BlockData : ScriptableObject
    {
        public BlockItemSO[] Blocks;
        
        /// <summary>Get block item by id.</summary>
        public BlockItemSO GetBlockItem(long id)
        {
            foreach (var blockItem in Blocks)
            {
                if (blockItem.Id == id)
                {
                    return blockItem;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class BlockItemSO
    {
        public long   Id;
        public Sprite Icon;
        public Block  Block;
    }
}