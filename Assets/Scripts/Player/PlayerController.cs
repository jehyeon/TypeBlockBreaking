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
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Fire type
            player.ChangeType(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Ice type
            player.ChangeType(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Wood type
            player.ChangeType(2);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Wood type
            player.Skill();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Attack
            player.Attack();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Guard
            player.Guard();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            // Guard
            player.CancelGuard();
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
