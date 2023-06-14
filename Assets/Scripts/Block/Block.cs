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
    [SerializeField]
    private Material[] mat = new Material[3];

    public void ChangeType(BlockType type)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat[(int)type];
    }
    public void ChangeType(int type)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat[type];
    }
}
