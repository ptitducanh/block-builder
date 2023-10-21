using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boostrap : MonoBehaviour
{
    [SerializeField] private IBootstrapLoader[] _bootstrapLoaders;
    
    private IEnumerator Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            yield return child.GetComponent<IBootstrapLoader>().Load();
        }
    }
}
