public interface IBlockPool
{
    public Block GetBlock(long blockId);
    public void ReturnBlock(Block block);
}