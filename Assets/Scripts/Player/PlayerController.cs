using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        TempInput();
    }

    void TempInput()
    {
        // for test
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Attack
            player.Attack();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.MoveRight();
        }

    }
}
