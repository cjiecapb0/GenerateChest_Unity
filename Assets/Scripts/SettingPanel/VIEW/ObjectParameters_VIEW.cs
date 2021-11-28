using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParameters_VIEW : MonoBehaviour
{
    private GameObject content;

    public ObjectParameters_VIEW(Dictionary<string, GameObject> prefabs)
    {
        this.content = prefabs.ContainsKey("content")
            ? prefabs["content"]
            : null;
    }

    public void RenderCard()
    {
        string formFillingPrefabPath = "Prefabs/FormFilling";

        GameObject template = MonoBehaviour.Instantiate(Resources.Load<GameObject>(formFillingPrefabPath)) as GameObject;
        template.transform.SetParent(content.transform, false);
    }
}
