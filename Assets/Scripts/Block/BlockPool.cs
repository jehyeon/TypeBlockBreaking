using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObject;
    [SerializeField]
    private int initCount = 0;

    Queue<Block> queue = new Queue<Block>();

    private static BlockPool instance;
    public static BlockPool Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }


    public void Init()
    {
        for (int i = 0; i < initCount; i++)
        {
            queue.Enqueue(CreateBlock());
        }
    }

    Block CreateBlock()
    {
        Block block = Instantiate(poolingObject).GetComponent<Block>();
        block.gameObject.SetActive(false);
        block.transform.SetParent(transform);

        return block;
    }

    public Block GetBlock()
    {
        Block block = Instance.queue.Count > 0
            ? Instance.queue.Dequeue()
            : CreateBlock();

        block.gameObject.SetActive(true);
        block.transform.SetParent(null);

        return block;
    }

    public void ReturnBlock(Block block)
    {
        block.gameObject.SetActive(false);
        block.transform.SetParent(Instance.transform);
        Instance.queue.Enqueue(block);
    }
}
