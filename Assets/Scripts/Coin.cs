using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,ICollectable
{
    [SerializeField] private int value;
    public static event Action<int> OnCoinCollected;
    public void Collect()
    {
       gameObject.SetActive(false);
       OnCoinCollected?.Invoke(value);
    }
}
