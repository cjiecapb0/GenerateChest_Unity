using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateCanvas_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ContentSizeFitter container;

    public void OnPointerClick(PointerEventData eventData)
    {
        ForceUpdateElements();
    }
    private void ForceUpdateElements()
    {   
        container.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        container.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}
