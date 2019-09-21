using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastleHealthBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI castleHealthText;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        GlobalEvents.OnCastleDamage += UpdateUI;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnCastleDamage -= UpdateUI;
    }

    public void UpdateUI(object sender, System.EventArgs e)
    {
        CastleDamageArgs arg = (CastleDamageArgs)e;
        slider.value = (float)arg.currentCastleHealth / arg.maxHealth;
    }
}
