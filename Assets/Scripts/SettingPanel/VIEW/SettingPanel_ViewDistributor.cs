using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel_ViewDistributor : MonoBehaviour
{
    public ObjectParameters_VIEW objectParameters_VIEW;
    public SettingPanel_ViewDistributor(Dictionary<string, Dictionary<string, GameObject>> prefabsReporitory)
    {
        Dictionary<string, GameObject> objectParametersPrefabs = prefabsReporitory.ContainsKey("objectParameters")
            ? prefabsReporitory["objectParameters"]
            : null;
        this.objectParameters_VIEW = new ObjectParameters_VIEW(objectParametersPrefabs);
    }
}
