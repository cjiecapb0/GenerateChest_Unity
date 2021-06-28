using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChest_ViewDistributor : MonoBehaviour
{
    public GenerateChest_VIEW generateChest_VIEW;
    public GenerateChest_ViewDistributor(Dictionary<string, Dictionary<string, List<GameObject>>> prefabsListReporitory)
    {
        Dictionary<string, List<GameObject>> generateChestPrefabs = prefabsListReporitory.ContainsKey("generateChest")
            ? prefabsListReporitory["generateChest"]
            : null;
        this.generateChest_VIEW = new GenerateChest_VIEW(generateChestPrefabs);
    }
}
