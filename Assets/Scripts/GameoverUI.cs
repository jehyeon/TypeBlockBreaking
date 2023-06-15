using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundText;

    public void Gameover(int round)
    {
        roundText.text = string.Format("\nRound: {0}", round);
    }
}
