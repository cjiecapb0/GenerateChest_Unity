using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGenerator_Root : MonoBehaviour
{
    [SerializeField] private List<GameObject> togglesCountResources;
    [SerializeField] private List<GameObject> togglesSelectedChest;
    [SerializeField] private List<GameObject> fixedResources;
    [SerializeField] private List<GameObject> selectedResourcesPrefabs;
    [SerializeField] private List<GameObject> prices;

    public void Awake()
    {
        Dictionary<string, Dictionary<string, List<GameObject>>> prefabReporitory = new Dictionary<string, Dictionary<string, List<GameObject>>>();
        Dictionary<string, List<GameObject>> prefabsList = new Dictionary<string, List<GameObject>>
            {
                { "togglesCountResources", togglesCountResources},
                { "togglesSelectedChest", togglesSelectedChest },
                { "fixedResources", fixedResources },
                { "selectedResourcesPrefabs", selectedResourcesPrefabs },
                { "prices", prices }
            };

        prefabReporitory.Add("generateChest", prefabsList);

        ChestGenerator_Manager.CreateObj(prefabReporitory);
    }
}
