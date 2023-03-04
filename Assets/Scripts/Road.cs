using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private RoadBlock blockPrefab;
    [SerializeField] private int blockCount;

    private Queue<RoadBlock> activeBlockStack = new Queue<RoadBlock>();
    private Vector3 spawnPos = Vector3.zero;
    private float lastBlockLength = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < blockCount; i++)
        {
            SpawnBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int blocksPassedBeforeSpawnRemove = 3;
        if (Camera.main.transform.position.z - lastBlockLength * blocksPassedBeforeSpawnRemove  > spawnPos.z - lastBlockLength * activeBlockStack.Count)
        {
            SpawnBlock();
            RemoveBlock();
        }
    }
    public void RemoveBlock()
    {
       RoadBlock roadBlock = activeBlockStack.Dequeue();
     //  roadBlock.gameObject.SetActive(false); 
       Destroy(roadBlock.gameObject); 
    }
    public void SpawnBlock()
    {
        RoadBlock block = Instantiate(blockPrefab);
        block.transform.position = spawnPos;
        //blocks.Add(block);
        block.transform.SetParent(transform);   
        activeBlockStack.Enqueue(block);   
        lastBlockLength = block.Length; 
        spawnPos.z += lastBlockLength;
    }
}
