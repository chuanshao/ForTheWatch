using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Singleton : MonoBehaviour
{
    private static GameObject _instance;
    public static GameObject Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("Singleton");
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
}
