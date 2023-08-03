using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUnitCanvas : MonoBehaviour
{
    public Image icon;
    public Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

    }

    public void Disableinfo()
    {
        canvas.enabled = false;
    }

    public void EnableInfo(Sprite icon)
    {
        this.icon.overrideSprite = icon;
        canvas.enabled = true;
    }

}
