using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEvents
{
    private Unit selectUnit;
    public InfoUnitCanvas infoUnitCanvas;

    Camera cameraPlayer;
    GameObject unitMirror;

    bool isNeedMirrorUnit;

    public PlayerEvents(InfoUnitCanvas infoUnitCanvas, Camera cameraPlayer, GameObject unitMirror)
    {
        isNeedMirrorUnit = false;
        selectUnit = null;
        this.infoUnitCanvas = infoUnitCanvas;
        this.cameraPlayer = cameraPlayer;
        this.unitMirror = unitMirror;
    }

    public void UpdatePlayerEvents()
    {
        if (isNeedMirrorUnit)
        {
            Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                unitMirror.transform.position = hit.point;
            }
        }
    }

    public void LeftClickMouse()
    {
        if (!isNeedMirrorUnit)
            CheckSelectCharacter();
        else if (isNeedMirrorUnit)
        {
            selectUnit.MoveToPoint(unitMirror.transform.position);
            isNeedMirrorUnit = false;
            unitMirror.SetActive(false);
        }


    }

    public void CheckSelectCharacter()
    {
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Unit hitUnit = hit.collider.GetComponentInParent<Unit>();
            if (hitUnit != null)
            {
                selectUnit = hit.collider.GetComponentInParent<Unit>();
                infoUnitCanvas.EnableInfo(selectUnit.Icon);

            }
            else if (selectUnit != null)
            {

                //infoUnitCanvas.Disableinfo();
                //selectUnit = null;
            }
        }

    }

    public void ClickedCommandMove()
    {
        isNeedMirrorUnit = true;
        unitMirror.SetActive(true);
    }
}
