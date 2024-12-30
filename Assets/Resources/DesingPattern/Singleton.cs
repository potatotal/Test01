using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            // 저는 이거 생략해요
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.AddComponent<T>();
            }
            
            return instance;
        }
    }

    // 게임이 시작되면 그냥 처음으로 불리우는 함수이다.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnBeforeSceneLoadRuntimeMethod()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }

        OnAwake();
    }

    public virtual void OnAwake()
    {
    }
}