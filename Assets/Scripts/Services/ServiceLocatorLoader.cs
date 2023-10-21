using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour, IBootstrapLoader
{
    public IEnumerator Load()
    {
        ServiceLocator locator = new();
        ServiceLocator.Register<ISaveLoadSystem>(new SaveLoadSystem());
        yield return null;
    }
}
