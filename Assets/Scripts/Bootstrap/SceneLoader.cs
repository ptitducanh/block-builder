using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IBootstrapLoader
{
    [SerializeField] private int _sceneIndex;
    public IEnumerator Load()
    {
        SceneManager.LoadScene(_sceneIndex, LoadSceneMode.Additive);
        yield return null;
    }
}
