using System;

namespace BuilderSystem
{
    public interface IBuilder
    {
        /// <summary>When player select a block from the inventory, the block will be set to the builder. Ready to be placed.</summary>
        public void SelectBlock(long blockId);
        
        /// <summary>When player placed a block on the ground. We'll need to remove that block from inventory</summary>
        public Action<Block> OnPlayerPlacedBlock { get; set; }

        /// <summary>When player tries to retrieve a block, the block will be retrieved from the world. Then add to the inventory. </summary>
        public Action<Block> OnPlayerRetrievedBlock { get; set; }
    }
}