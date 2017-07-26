using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    [HideInInspector]
    public static bool isFirst;
    [HideInInspector]
    public static bool isMenuTips;
    [HideInInspector]
    public static bool isEggTips;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        isFirst = true;
        isMenuTips = true;
        isEggTips = true;
        DontDestroyOnLoad(gameObject);
    }
}
