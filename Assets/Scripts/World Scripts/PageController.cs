using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    //TMP text component
    private TMP_Text PageText;

    private void Awake()
    {
        PageText = GetComponent<TMP_Text>();
    }

    public void SetPageText(string newText)
    {
        PageText.text = newText;
    }

    public int GetPageCount()
    {
        PageText.ForceMeshUpdate();
        return PageText.textInfo.pageCount;
    }

    public void ShowPage(int pageIndex)
    {
        PageText.pageToDisplay = pageIndex;
    }
}
