using UnityEngine;

public class SaveLoadSystem : ISaveLoadSystem
{
    public void SaveData<T>(T data)
    { 
        // serialize data using Unity's JsonUtility.ToJson() method.
        string jsonString = JsonUtility.ToJson(data);
        // save the serialized data to a file using Unity's File.WriteAllText() method.
        System.IO.File.WriteAllText(Application.persistentDataPath + $"/{typeof(T)}.json", jsonString);
    }

    public T LoadData<T>()
    {
        // check if the file exists using Unity's File.Exists() method.
        if (!System.IO.File.Exists(Application.persistentDataPath + $"/{typeof(T)}.json"))
        {
            return default;
        }

        // load the serialized data from a file using Unity's File.ReadAllText() method.
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + $"/{typeof(T)}.json");
        // deserialize the data using Unity's JsonUtility.FromJson<T>() method.
        return JsonUtility.FromJson<T>(jsonString);
    }
}