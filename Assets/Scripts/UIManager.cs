using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundText;      // 라운드
    [SerializeField]
    private Slider hpSlider;                // 체력

    private static UIManager instance = null;
    public static UIManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateRoundUI(int round)
    {
        roundText.gameObject.SetActive(true);
        roundText.text = string.Format("Round {0}", round);
    }

    public void UpdateHPSlider(int now, int max)
    {
        hpSlider.value = (float)now / (float)max;
    }
}
