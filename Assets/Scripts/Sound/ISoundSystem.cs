public enum SFX
{
    PlaceBlock,
    RetrieveBlock,
}
    
public interface ISoundSystem
{
    public void PlaySFX(SFX sfx);
}