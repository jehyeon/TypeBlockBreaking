using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
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

    // Managers
    public BlockManager BlockManager;

    private int round = 0;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Round
    private void StartRound()
    {
    }

    public void EndRound()
    {
        round++;
    }
}
