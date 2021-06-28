using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedResource_BEHAVIOR : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private GameObject countResource;
    [SerializeField] private GameObject nameResource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChestGenerator_Manager.RenderToolTip(this.gameObject, this.countResource, nameResource, this.toolTip);
        this.toolTip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.toolTip.SetActive(false);
    }
}
