using System;
using System.Collections.Generic;

/// <summary>
/// The service locator is for storing all the services.
/// Each service can be accessed by its type, and can be used by any other classes.
/// </summary>
public class ServiceLocator
{
    // A simple singleton pattern.
    private static ServiceLocator _instance;
    private static ServiceLocator Instance => _instance ??= new ServiceLocator();
    
    private Dictionary<Type, object> _services = new();

    public ServiceLocator()
    {
        _instance = this;
    }

    public static void Register<T>(T service)
    {
        Instance._services.Add(typeof(T), service);
    }
        
    public static T Get<T>()
    {
        return (T) Instance._services[typeof(T)];
    }
}