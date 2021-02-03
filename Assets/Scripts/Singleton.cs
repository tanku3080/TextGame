﻿using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);
                instance = (T)FindObjectOfType(t);
                if (instance == null) Debug.LogError(t + "をアタッチしているオブジェクトがありません");
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {
        if (this != Instance)
        {
            Debug.Log("同一オブジェクトがアタッチされています" + Instance.gameObject.name);
            Destroy(this.gameObject);
            return;
        }
    }
}

