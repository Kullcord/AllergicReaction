using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject petMenuBar;
    [SerializeField] private GameObject indicators;

    [SerializeField] private Button infoBtn;
    [SerializeField] private Sprite infoImg;
    [SerializeField] private Sprite xImg;

    private bool action = false;//if false open indicators else open bar

    private void Start()
    {
        infoBtn.image.sprite = infoImg;

        petMenuBar.SetActive(false);
        indicators.SetActive(true);
    }

    public void InfoButton()
    {
        action = !action;

        if (!action)
        {
            infoBtn.image.sprite = infoImg;

            petMenuBar.SetActive(false);
            indicators.SetActive(true);
        } 
        else
        {
            infoBtn.image.sprite = xImg;

            petMenuBar.SetActive(true);
            indicators.SetActive(false);
        }
    }
}
