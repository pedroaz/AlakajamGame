using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPanel : MonoBehaviour
{

    public List<GameObject> listOfIcons;
    public GameObject container;
    public TextMeshProUGUI panelText;
    

    public void ShowItemPanel(int index, string t)
    {
        ShowIcon(index);
        StartCoroutine(ShowPanel());
        panelText.text = t;
    }

    private void ShowIcon(int index)
    {
        foreach (GameObject go in listOfIcons) {
            go.SetActive(false);
        }

        listOfIcons[index].SetActive(true);
    }

    private IEnumerator ShowPanel(){
        container.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        container.SetActive(false);
    }
}
