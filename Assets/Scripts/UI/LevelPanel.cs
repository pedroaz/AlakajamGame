using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPanel : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;


    private void Awake()
    {

    }

    public void ShowPanel(int level)
    {
        text.text = "Level: " + level.ToString();
        StartCoroutine(Disapear());   
    }

    private IEnumerator Disapear()
    {
        print("SHOW PANEL");
        panel.SetActive(true);

        yield return new WaitForSeconds(2);

        panel.SetActive(false);
        print("HIDE PANEL");
    }
}
