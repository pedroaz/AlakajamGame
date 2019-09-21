using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPanel : MonoBehaviour
{
    Image image;
    public TextMeshProUGUI text;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ShowPanel(int level)
    {
        text.text = "Level: " + level.ToString();
        StartCoroutine(Disapear());   
    }

    private IEnumerator Disapear()
    {
        var tempColor = image.color;
        var textColor = text.color;


        textColor.a = 255;
        text.color = textColor;

        tempColor.a = 255;
        image.color = tempColor;

        yield return new WaitForSeconds(2);

        tempColor.a = 0;
        image.color = tempColor;

        tempColor.a = 0;
        text.color = tempColor;
    }
}
