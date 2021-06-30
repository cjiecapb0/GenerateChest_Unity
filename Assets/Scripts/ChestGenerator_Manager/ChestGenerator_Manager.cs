using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class ChestGenerator_Manager
{

    private static GenerateChest_ViewDistributor VIEW;

    public static void CreateObj(Dictionary<string, Dictionary<string, List<GameObject>>> prefabsListReporitory)
    {
        VIEW = new GenerateChest_ViewDistributor(prefabsListReporitory);

        FillFixedResources();
    }

    public static void FillFixedResources()
    {
        VIEW.generateChest_VIEW.FillFixedResources();
    }

    public static void GenerateResources()
    {
        VIEW.generateChest_VIEW.GenerateResources();
    }

    public static void СopyСlipboardChest()
    {
        VIEW.generateChest_VIEW.СopyСlipboardChest();
    }

    public static void RenderToolTip(string countResource, string nameResource, GameObject toolTip)
    {
        VIEW.generateChest_VIEW.RenderToolTip(countResource, nameResource, toolTip);
    }

    public static void CalculatingCostCheast()
    {
        VIEW.generateChest_VIEW.CalculatingCostCheast();
    }

    public static void ShowChest(GameObject prices, GameObject showWindow)
    {
        VIEW.generateChest_VIEW.ShowChest(prices, showWindow);
    }

    public static void CopyResoursce(string nameResource, string countResource)
    {
        VIEW.generateChest_VIEW.CopyResoursce(nameResource, countResource);
    }

    public static void RefreshResoursce(GameObject gameObject)
    {
        VIEW.generateChest_VIEW.RefreshResoursce(gameObject);
    }

    public static void DeleteSelectedResourses(string nameResource)
    {
        VIEW.generateChest_VIEW.DeleteSelectedResourses(nameResource);
    }

    public static void SetCountResource(string name, int count)
    {
        VIEW.generateChest_VIEW.SetCountResource(name, count);
    }
}
