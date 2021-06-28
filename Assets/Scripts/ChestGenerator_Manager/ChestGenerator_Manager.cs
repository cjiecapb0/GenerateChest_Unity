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
    public static void RenderToolTip(GameObject selectedResource, GameObject countResource, GameObject name, GameObject toolTip)
    {
        VIEW.generateChest_VIEW.RenderToolTip(selectedResource, countResource, name, toolTip);
    }

    public static void CalculatingCostCheast()
    {
        VIEW.generateChest_VIEW.CalculatingCostCheast();
    }
    public static void ShowChest(GameObject prices, GameObject showWindow)
    {
        VIEW.generateChest_VIEW.ShowChest(prices, showWindow);
    }
}

//public static void GenerateResources(StackPanel checkBox, ComboBox comboBox, TextBlock text, TextBlock textJ, List<Resource> rootResources)
//{
//    int number = 1;
//    foreach (UIElement box in checkBox.Children)
//    {
//        if (box is RadioButton button)
//        {
//            if ((bool)button.IsChecked)
//            {
//                number = Convert.ToInt32(button.Content);
//            }
//        }
//    }
//    int[] numbersRes = RandomResource(number, rootResources.Count);

//    List<Resource> selestedResources = new List<Resource>();

//    for (int i = 0; i < numbersRes.Length; i++)
//    {
//        Resource resource = rootResources[numbersRes[i]];
//        selestedResources.Add(resource);
//    }
//    if (comboBox.Text == "none")
//        PrintingResources(selestedResources, numbersRes.Length, text, textJ);
//    else
//        CheckingResources(selestedResources, numbersRes.Length, text, textJ, comboBox, rootResources);
//}
//private static void CheckingResources(List<Resource> selestResources, int count, TextBlock text, TextBlock textJ, ComboBox comboBox, List<Resource> rootResources)
//{
//    int index = 0;
//    for (int i = 0; i < selestResources.Count; i++)
//    {
//        if (selestResources[i].Name == comboBox.Text) break;
//        else index++;
//    }

//    if (index == selestResources.Count)
//    {
//        selestResources.Remove(selestResources[^1]);
//        foreach (Resource item in rootResources)
//        {
//            if (item.Name == comboBox.Text) selestResources.Add(item);
//        }
//    }
//    PrintingResources(selestResources, count, text, textJ);
//}
//private static int[] RandomResource(int countResourses, int allResourses)
//{
//    Random rand = new Random();
//    int[] numbers = new int[countResourses];
//    numbers[0] = rand.Next(0, allResourses);
//    for (int i = 1; i < numbers.Length;)
//    {
//        int num = rand.Next(0, allResourses);
//        int j;
//        for (j = 0; j < i; j++)
//        {
//            if (num == numbers[j]) break;
//        }
//        if (j == i)
//        {
//            numbers[i] = num;
//            i++;
//        }
//    }
//    return numbers;
//}
//private static void PrintingResources(List<Resource> selestResources, int count, TextBlock text, TextBlock textJ)
//{
//    text.Text = "";
//    textJ.Text = $"(";
//    for (int i = 0; i < selestResources.Count; i++)
//    {
//        Run run = new Run { Text = $"{selestResources[i].Name}\n" };
//        if (selestResources[i].Level == 2)
//            run.Foreground = Brushes.BlueViolet;
//        if (selestResources[i].Level == 3)
//            run.Foreground = Brushes.Blue;
//        if (selestResources[i].Level == 4)
//            run.Foreground = Brushes.Red;
//        text.Inlines.Add(run);

//        textJ.Text += $"(ResourceId=\"{selestResources[i].Id}\",Count={selestResources[i].Count})";
//        if (i != count - 1) textJ.Text += ",";
//    }
//    textJ.Text += ")";
//}
//private void RenderComboBoxItems(Root root)
//{
//    foreach (Resource item in root.ResourcesT1)
//    {
//        ComboBoxItem comboBoxItem = new ComboBoxItem { Content = item.Name };
//        comboBoxT1.Items.Add(comboBoxItem);
//    }
//    foreach (Resource item in root.ResourcesT2)
//    {
//        ComboBoxItem comboBoxItem = new ComboBoxItem { Content = item.Name };
//        comboBoxT2.Items.Add(comboBoxItem);
//    }
//    foreach (Resource item in root.ResourcesT3)
//    {
//        ComboBoxItem comboBoxItem = new ComboBoxItem { Content = item.Name };
//        comboBoxT3.Items.Add(comboBoxItem);
//    }
//    foreach (Resource item in root.ResourcesT4)
//    {
//        ComboBoxItem comboBoxItem = new ComboBoxItem { Content = item.Name };
//        comboBoxT4.Items.Add(comboBoxItem);
//    }
//}
//    }
