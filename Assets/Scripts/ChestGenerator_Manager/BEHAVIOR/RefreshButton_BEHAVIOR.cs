using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RefreshButton_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ChestGenerator_Manager.RefreshResoursce(gameObject);
        ChestGenerator_Manager.CalculatingCostCheast();
    }
}
