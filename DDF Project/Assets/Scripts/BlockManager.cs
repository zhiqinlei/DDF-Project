using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BlockManager : MonoBehaviour
{
	[Required] public GameObject XRangeMax;
    [Required] public GameObject XRangeMin;
    [Required] public GameObject ZRangeMax;
    [Required] public GameObject ZRangeMin;
    [Required] public GameObject BlockGroup; 
    [Required] public GameObject BlockPrefab;
    public float BlockExistingTime;
    public float BlockGenerateInterval;
    private float tempBlockGenerateInterval;
    public float Size;
    public bool AutoGenerateBlock = true; 
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        tempBlockGenerateInterval = BlockGenerateInterval + 10;
    }

    void Update()
    {
        if (gameManager.GameMode == GameManager.Mode.Normal)
        {
            NormalGameModeLoop();
        }
    }

    private void NormalGameModeLoop()
    {
        if (AutoGenerateBlock)
        {
            tempBlockGenerateInterval -= Time.deltaTime;
            if (tempBlockGenerateInterval <= 0.0f)
            {
                GenerateBlock();
                tempBlockGenerateInterval = BlockGenerateInterval;
            }
        }
    }

    public void GenerateBlock()
    {
        Size = Random.Range(2.5f, 5.5f);
        Vector3 randomSize = new Vector3 (Size, 1.0f, 0.6f);
        Vector3 startPos = new Vector3(
            Random.Range(XRangeMin.transform.position.x, XRangeMax.transform.position.x),
            BlockGroup.transform.position.y,
            Random.Range(ZRangeMin.transform.position.z, ZRangeMax.transform.position.z)
        );
        GameObject BlockObj = Instantiate(BlockPrefab, startPos, Quaternion.identity, BlockGroup.transform);
        BlockController Block = BlockObj.GetComponent<BlockController>();
        BlockObj.transform.localScale = randomSize;
        Block.Initialize(BlockExistingTime);
    }
}
