using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private Blocks blocks;

    void Start()
    {
        CreateBlocks(5);
    }

    public void CreateBlocks(int floor)
    {
        int yPos = 0;
        while (floor-- > 0)
        {
            int xPos = -2;
            for (int i = 0; i < 3; i++)
            {
                Block block = BlockPool.Instance.GetBlock();
                block.ChangeType(Random.Range(0, 3));
                block.transform.SetParent(blocks.transform);
                block.transform.position = new Vector3(xPos, yPos, 0);

                xPos += 2;
            }

            yPos++;
        }
    }
}
