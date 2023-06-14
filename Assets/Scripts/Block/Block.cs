using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Fire = 0,
    Ice = 1,
    Wood = 2
}

public class Block : MonoBehaviour
{
    private int hp = 14;

    [SerializeField]
    private Material[] mat = new Material[3];

    public BlockType Type;

    public void ChangeType(BlockType type)
    {
        Type = type;
        gameObject.GetComponent<MeshRenderer>().material = mat[(int)type];
    }
    public void ChangeType(int type)
    {
        Type = (BlockType)type;
        gameObject.GetComponent<MeshRenderer>().material = mat[type];
    }

    public bool Attacked(int damage)
    {
        hp -= damage;

        return hp < 0;
    }
}
