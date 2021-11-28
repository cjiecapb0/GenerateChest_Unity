using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel_Root : MonoBehaviour
{
    [SerializeField] private GameObject contentCharacters;
    [SerializeField] private GameObject contentStatics;
    [SerializeField] private GameObject contentRestores;
    [SerializeField] private GameObject contentPlants;
    [SerializeField] private GameObject contentRentHouses;
    [SerializeField] private GameObject contentFactories;
    [SerializeField] private GameObject contentGates;
    [SerializeField] private GameObject contentTreasures;
    [SerializeField] private GameObject contentGarbages;
    [SerializeField] private GameObject contentResources;
    [SerializeField] private GameObject contentDecorations;
    [SerializeField] private GameObject contentAchievementSettings;

    public void Awake()
    {
        Dictionary<string, Dictionary<string, GameObject>> prefabReporitory = new Dictionary<string, Dictionary<string, GameObject>>();
        Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>
            {
                { "contentCharacters", contentCharacters},
                { "contentStatics", contentStatics},
                { "contentRestores", contentRestores},
                { "contentPlants", contentPlants},
                { "contentRentHouses", contentRentHouses},
                { "contentFactories", contentFactories},
                { "contentGates", contentGates},
                { "contentTreasures", contentTreasures},
                { "contentGarbages", contentGarbages},
                { "contentResources", contentResources},
                { "contentDecorations", contentDecorations},
                { "contentAchievementSettings", contentAchievementSettings},
            };

        prefabReporitory.Add("objectParameters", prefabs);

        SettingPanel_Manager.CreateObj(prefabReporitory);
    }
}
