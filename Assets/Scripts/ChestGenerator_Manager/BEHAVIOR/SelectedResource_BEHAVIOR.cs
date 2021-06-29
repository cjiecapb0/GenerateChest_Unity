using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedResource_BEHAVIOR : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private GameObject countResource;
    [SerializeField] private GameObject nameResource;
    [SerializeField] private GameObject iconCopy;

    public void OnPointerClick(PointerEventData eventData)
    {
        ChestGenerator_Manager.CopyResoursce(gameObject, countResource);
        SetActiveTrueIconCopy();
        Invoke("SetActiveFalseIconCopy", 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChestGenerator_Manager.RenderToolTip(this.gameObject, this.countResource, nameResource, this.toolTip);
        this.toolTip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.toolTip.SetActive(false);
    }
    public void SetActiveTrueIconCopy()
    {
        iconCopy.SetActive(true);
    }
    public void SetActiveFalseIconCopy()
    {
        iconCopy.SetActive(false);
    }
}
