using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    public bool isHighLightOn;

    bool isHighlighted;
    public SpriteRenderer[] sprites;

    public Material materialDefault;
    public Material[] materialHighlihgtOnOff;

    public float highlightInterval;
    float currentInterval;

    private void Update()
    {
        if(isHighLightOn)
        {
            if (currentInterval < highlightInterval)
            {
                currentInterval += Time.deltaTime;
            }
            else
            {
                ToggleHighlight();
                currentInterval = 0;
            }
        }
        
    }

    void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
        if(isHighlighted)
        {
            foreach (var item in sprites)
            {
                item.sharedMaterial = materialHighlihgtOnOff[0];
            }
        }
        else
        {
            foreach (var item in sprites)
            {
                item.sharedMaterial = materialHighlihgtOnOff[1];
            }
        }
    }

    public void SetOff()
    {
        isHighLightOn = false;
        foreach (var item in sprites)
        {
            item.sharedMaterial = materialDefault;
        }
    }
}
