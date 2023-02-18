using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    [field: SerializeField] public Transform Begin { get; private set; }
    [field: SerializeField] public Transform End { get; private set; }
    public float Length { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        Length = (End.position - Begin.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
