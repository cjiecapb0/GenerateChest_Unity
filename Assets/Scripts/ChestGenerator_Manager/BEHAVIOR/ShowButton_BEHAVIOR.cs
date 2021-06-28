using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowButton_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject prices;
    [SerializeField] private GameObject showWindow;
    [SerializeField] private GameObject generate;

    public void OnPointerClick(PointerEventData eventData)
    {
        ChestGenerator_Manager.ShowChest(prices, showWindow);
    }
    public void PricesPosition()
    {
        prices.transform.SetParent(generate.transform);
    }
}
