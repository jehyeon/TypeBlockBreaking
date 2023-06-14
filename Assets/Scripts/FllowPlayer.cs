using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Vector3 offset;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            transform.position = new Vector3
            (
                offset.x, 
                player.transform.position.y + offset.y, 
                offset.z
            );
        }
    }
}
