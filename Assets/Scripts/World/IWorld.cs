using System.Collections.Generic;

public interface IWorld
{
    /// <summary> Add a block to the world </summary>
    public void AddBlock(Block block);
    /// <summary> Remove a block from the world </summary>
    public void RemoveBlock(Block block);
}