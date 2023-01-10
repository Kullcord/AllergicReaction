using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualScreenUIManager : MonoBehaviour
{
    public Slider hungerBar;
    public Slider thirstBar;
    public Slider boredomeBar;
    public Slider loveBar;

    private float maxHungerVal = 100;
    private float maxThirstVal = 100;
    private float maxBoredomeVal = 100;
    private float maxLoveVal = 100;

    public float atentionSpan;
    [SerializeField] private float currentTime;

    private void Update()
    {
        if(currentTime <= (atentionSpan * 100))
            currentTime += Time.deltaTime * 5;
        /*else*/

    }

    private void Start()
    {
        hungerBar.maxValue = maxHungerVal;
        thirstBar.maxValue = maxThirstVal;
        boredomeBar.maxValue = maxBoredomeVal;
        loveBar.maxValue = maxLoveVal;
    }

    public void SetStats(float hunger, float thirst, float boredome, float love, float atention)
    {
        hungerBar.value = hunger;
        thirstBar.value = thirst;
        boredomeBar.value = boredome;
        loveBar.value = love;

        atentionSpan = atention;
    }


}
