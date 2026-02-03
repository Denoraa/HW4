using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name + " (MonoSingleton)");
                    instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            instance = this as T;

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        OnAwake();
    }


    protected virtual void OnAwake() { return; }



}