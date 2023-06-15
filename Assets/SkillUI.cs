using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] percent = new GameObject[4];

    public void UpdatePercent(int count)
    {
        for (int i = 0; i < 4; i++)
        {
            percent[i].SetActive(false);
        }

        int now = (float)count / 3 > 4.0f
            ? 4
            : (int)((float)count / 3);

        for (int i = 0; i < now; i++)
        {
            percent[i].SetActive(true);
        }
    }
}
