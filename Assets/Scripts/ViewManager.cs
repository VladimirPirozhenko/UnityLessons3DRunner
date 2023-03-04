using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private List<BaseView> views;

    static public ViewManager instance;
    void Awake()
    {
        instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
