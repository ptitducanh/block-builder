using System.Collections;

public interface IBootstrapLoader
{
    /// <summary>Load and initialize the data needed for the system to work properly.</summary>
    public IEnumerator Load();
}
