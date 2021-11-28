using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddFormFilling_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ClassObject classObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        SettingPanel_Manager.RenderCard(classObject);
        this.gameObject.transform.SetAsLastSibling();
    }
}
