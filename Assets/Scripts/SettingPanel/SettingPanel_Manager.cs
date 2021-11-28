using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class SettingPanel_Manager
{

    private static SettingPanel_ViewDistributor VIEW;

    public static void CreateObj(Dictionary<string, Dictionary<string, GameObject>> prefabsReporitory)
    {
        VIEW = new SettingPanel_ViewDistributor(prefabsReporitory);

        VIEW.objectParameters_VIEW.StartRender();
    }

    public static void RenderCard(ClassObject classObject)
    {
        VIEW.objectParameters_VIEW.AddCardInDeck(classObject);
    }
}
