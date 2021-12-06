using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public RectTransform CoinTarget;
    
    private void Awake() => Instance = this;

   
}
