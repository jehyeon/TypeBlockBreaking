using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    private BoxCollider col;

    private BlockManager blockManager;

    private Queue<Block> blocks = new Queue<Block>();
    private Block[] target = new Block[3];

    private Rigidbody rigid;

    public float Power { get { return rigid.velocity.y * -1; } }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();

        blockManager = GameManager.Instance.BlockManager;
    }

    public void AddBlock(Block block)
    {
        blocks.Enqueue(block);
    }

    public void Fall()
    {
        transform.position = blockManager.BlocksSpawnPos;
        col.center = Vector3.zero;

        rigid.useGravity = true;

        for (int i = 0; i < 3; i++)
        {
            target[i] = blocks.Dequeue();
        }

        gameObject.SetActive(true);
    }

    public void Attacked(int index, int damage)
    {
        // index 0부터 왼쪽
        if (target[index].Attacked(damage))
        {
            // target block hp < 0이면
            for (int i = 0; i < 3; i++)
            {
                BlockPool.Instance.ReturnBlock(target[i]);
            }
            col.center = new Vector3(col.center.x, col.center.y + 1, col.center.z);

            if (blocks.Count < 1)
            {
                // block이 없는 경우
                Clear();
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    target[i] = blocks.Dequeue();
                }
            }
        }
    }

    public void Guarded(float guardPower)
    {
        rigid.AddForce(Vector3.up * guardPower, ForceMode.Impulse);
    }

    private void Clear()
    {
        gameObject.SetActive(false);

        GameManager.Instance.EndRound();
    }
}
