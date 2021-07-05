using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if(instance == null)
                Debug.LogError("PoolManager instance not found");

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
