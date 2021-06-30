using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteButton_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        GameObject infoResource = parent.transform.Find("infoResource").gameObject;
        string name = infoResource.transform.Find("nameResource").gameObject.GetComponent<TextMeshProUGUI>().text;
        ChestGenerator_Manager.DeleteSelectedResourses(name);
        ChestGenerator_Manager.CalculatingCostCheast();
    }
}
