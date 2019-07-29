using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Text info;
    [SerializeField] private Text header;

    [SerializeField] private string[] infos;
    [SerializeField] private string[] headers;

    [SerializeField] private Sprite[] indexes;
    [SerializeField] private Sprite[] tutorials;

    [SerializeField] private Image index;
    [SerializeField] private Image tutorial;

    [SerializeField] private GameObject previous;

    public int pageNumber = 0;
    #endregion

    #region MONOBEHAVIOR METHODS
    void Update ()
    {
        UpdateTexts();
        UpdateSprites();
        SwitchPreviousButton();
    }
    #endregion

    #region CUSTOM METHODS
    void UpdateTexts ()
    {
        index.sprite    = indexes[pageNumber];
        tutorial.sprite = tutorials[pageNumber];
    }

    void UpdateSprites()
    {
        info.text   = infos[pageNumber];
        header.text = headers[pageNumber];
    }

    // ON/OFF BASED ON PAGES
    void SwitchPreviousButton ()
    {
        if (pageNumber == 0)
        {
            previous.SetActive(false);
        }
        else
        {
            previous.SetActive(true);
        }
    }

    public void Next ()
    {
        if (pageNumber < 4)
        {
            pageNumber++;
        }
        else
        {
            Close();
        }
    }

    public void Previous ()
    {
        if (pageNumber > 0) pageNumber--;
    }

    public void Close ()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
