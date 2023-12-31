using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private Blocks blocks;

    public Vector3 BlocksSpawnPos = new Vector3(0, 20.0f, 0);

    public void CreateBlocks(int floor, List<int> blockTypes)
    {
        int yPos = 0;
        while (floor-- > 0)
        {
            int xPos = -2;
            for (int i = 0; i < 3; i++)
            {
                Block block = BlockPool.Instance.GetBlock();
                int select = Random.Range(0, blockTypes.Count);
                block.ChangeType(blockTypes[select]);
                block.transform.SetParent(blocks.transform);
                block.transform.localPosition = new Vector3(xPos, yPos, 0);
                blocks.AddBlock(block);

                xPos += 2;
            }

            yPos++;
        }

        blocks.Fall();
    }
}
