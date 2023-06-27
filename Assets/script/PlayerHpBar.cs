using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Image HpBar;

    private shipPlayer shipPlayer;

    void Start()
    {
        shipPlayer = FindObjectOfType<shipPlayer>();
    }

    void Update()
    {
        HpBar.fillAmount = Mathf.Clamp01(shipPlayer.HpLeft);
    }
}
