using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private Player Player;

    void Awake()
    {
        Player = transform.parent.GetComponent<Player>();
    }

    public void On()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void Off()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blocks"))
        {
            Blocks blocks = other.GetComponent<Blocks>();
            if (!Player.GuardMode)
            {
                blocks.Attacked(Player.Index, Player.Type, Player.FireSwordMode);
            }
            else
            {
                Player.Guarded(blocks.Power);
                blocks.Guarded(Player.Power);
            }
        }
    }
}
