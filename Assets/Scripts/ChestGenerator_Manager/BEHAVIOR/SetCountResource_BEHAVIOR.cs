using System;
using TMPro;
using UnityEngine;

public class SetCountResource_BEHAVIOR : MonoBehaviour
{
    [SerializeField] private TMP_InputField countTMP;
    public void SetCountResource()
    {
        int count = Convert.ToInt32(countTMP.text);
        string name = gameObject.GetComponent<TextMeshProUGUI>().text;
        ChestGenerator_Manager.SetCountResource(name, count);
    }
}
