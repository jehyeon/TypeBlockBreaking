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
    public UIManager UIManager;

    Queue<List<int>> seq = new Queue<List<int>>();
    private List<int> blockTypes;
    private int floorCount = 5;
    private int round = 0;
    private int breakCount = 0;
    public int BreakCount { get { return breakCount; } }

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

    void Start()
    {
        seq.Enqueue(new List<int>() { 0 });
        seq.Enqueue(new List<int>() { 1 });
        seq.Enqueue(new List<int>() { 2 });
        seq.Enqueue(new List<int>() { 0, 1 });
        seq.Enqueue(new List<int>() { 0, 2 });
        seq.Enqueue(new List<int>() { 1, 2 });
        seq.Enqueue(new List<int>() { 0, 1, 2 });

        Invoke("StartRound", 3f);
    }

    // Round
    private void StartRound()
    {
        round++;
        UIManager.Instance.UpdateRoundUI(round);

        if (seq.Count > 0)
        {
            blockTypes = seq.Dequeue();
        }
        else
        {
            floorCount++;
        }

        BlockManager.CreateBlocks(floorCount, blockTypes);
    }

    public void EndRound()
    {
        Invoke("StartRound", 5f);
    }

    public void UpBreakBlockCount()
    {
        breakCount++;
        // UI 업데이트
    }

    public void ClearBreakBlockCount()
    {
        breakCount = 0;
        // UI 업데이트
    }
}
