using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{



    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                string n = typeof(T).Name;
                T results = Resources.Load<T>("Singletons/" + n);



                instance = results;
                if(instance is SingletonScriptableObject<T> script)
                {
                    script.OnInitialize();
                }







            }



            return instance;
        }
    }



    // Optional overridable method for initializing the instance.
    protected virtual void OnInitialize() { }



}
