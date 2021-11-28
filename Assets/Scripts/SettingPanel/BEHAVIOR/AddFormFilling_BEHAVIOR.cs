using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddFormFilling_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        string formFillingPrefabPath = "Prefabs/FormFilling";

        GameObject template = MonoBehaviour.Instantiate(Resources.Load<GameObject>(formFillingPrefabPath)) as GameObject;
        template.transform.SetParent(this.gameObject.transform.parent, false);
        this.gameObject.transform.SetAsLastSibling();
    }
}
