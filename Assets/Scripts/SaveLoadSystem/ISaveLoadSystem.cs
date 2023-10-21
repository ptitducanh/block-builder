public interface ISaveLoadSystem
{
    /// <summary>Save the given object to a json file.</summary>
    public void SaveData<T>(T data);
    
    /// <summary>Load the requested data from json file</summary>
    public T LoadData<T>();
}