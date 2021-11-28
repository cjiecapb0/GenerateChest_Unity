using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollapsePanel_BEHAVIOR : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject content;
    [SerializeField] private RectTransform image;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.content.activeSelf == true)
        {
            this.content.SetActive(false);
            this.image.rotation = new Quaternion(0, 0, -45, 1);
        }
        else
        {
            this.content.SetActive(true);
            this.image.rotation = new Quaternion(0, 0, 0, 1);
        }
    }
}
