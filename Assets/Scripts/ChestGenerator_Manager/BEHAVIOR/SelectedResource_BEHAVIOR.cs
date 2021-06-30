using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedResource_BEHAVIOR : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private TMP_InputField countResource;
    [SerializeField] private TextMeshProUGUI nameResource;
    [SerializeField] private GameObject iconCopy;

    public void OnPointerClick(PointerEventData eventData)
    {
        ChestGenerator_Manager.CopyResoursce(nameResource.text, countResource.text);
        SetActiveTrueIconCopy();
        Invoke(nameof(SetActiveFalseIconCopy), 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChestGenerator_Manager.RenderToolTip(countResource.text, nameResource.text, this.toolTip);
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
