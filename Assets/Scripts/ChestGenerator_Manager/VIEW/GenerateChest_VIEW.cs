using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class GenerateChest_VIEW
{
    private List<Resource> selestedResources;

    private List<GameObject> togglesCountResources;
    private List<GameObject> togglesSelectedChest;
    private List<GameObject> fixedResources;
    private List<GameObject> selectedResourcesPrefabs;
    private List<GameObject> prices;
    private List<GameObject> cells;

    private readonly RootResources rootResources;

    private string clipboardChest;
    private string clipboardResources;

    public GenerateChest_VIEW(Dictionary<string, List<GameObject>> prefabsList)
    {
        this.togglesCountResources = prefabsList.ContainsKey("togglesCountResources")
            ? prefabsList["togglesCountResources"]
            : null;

        this.togglesSelectedChest = prefabsList.ContainsKey("togglesSelectedChest")
            ? prefabsList["togglesSelectedChest"]
            : null;

        this.fixedResources = prefabsList.ContainsKey("fixedResources")
            ? prefabsList["fixedResources"]
            : null;

        this.selectedResourcesPrefabs = prefabsList.ContainsKey("selectedResourcesPrefabs")
            ? prefabsList["selectedResourcesPrefabs"]
            : null;

        this.prices = prefabsList.ContainsKey("prices")
            ? prefabsList["prices"]
            : null;

        this.cells = prefabsList.ContainsKey("cells")
            ? prefabsList["cells"]
            : null;

        TextAsset rootResourcesPath = Resources.Load<TextAsset>("data");
        rootResources = JsonConvert.DeserializeObject<RootResources>(rootResourcesPath.text);
        selestedResources = new List<Resource>();
        clipboardChest = "";
        clipboardResources = "";
    }
    public void FillFixedResources()
    {

        List<Resource> resources = GetSelectedChest();
        foreach (GameObject gameObject in this.fixedResources)
        {
            GameObject gameObjectDropdown = gameObject.transform.Find("Dropdown").gameObject;
            TMP_Dropdown dropdown = gameObjectDropdown.GetComponent<TMP_Dropdown>();

            dropdown.options.Clear();
            dropdown.options.Add(new TMP_Dropdown.OptionData { text = "none" });
            dropdown.transform.Find("Label").gameObject.GetComponent<TextMeshProUGUI>().text = "none";
            foreach (Resource resource in resources)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData { text = resource.Name });
            }
        }
    }
    public void GenerateResources()
    {
        int countResourcesInChest = GetCountResourcesInChest();
        int[] selectedNumberResources = RandomResource(countResourcesInChest, GetSelectedChest().Count);

        SetSelestedResources(selectedNumberResources);

        for (int j = 0; j < fixedResources.Count; j++)
        {
            GameObject dropdown = fixedResources[j].transform.Find("Dropdown").gameObject;
            GameObject label = dropdown.transform.Find("Label").gameObject;
            string nameFixRes = label.GetComponent<TextMeshProUGUI>().text;

            if (nameFixRes == "none")
                PrintingResources();
            else
                CheckingResources();
            CalculatingCostCheast();
        }
        PastedChest();
    }
    public void ShowChest(GameObject prices, GameObject showWindow)
    {
        foreach (GameObject cell in cells)
        {
            cell.SetActive(false);
        }
        for (int i = 0; i < selestedResources.Count; i++)
        {
            GameObject countResource = selectedResourcesPrefabs[i].transform.Find("countResource").gameObject;
            string resourceCount = countResource.GetComponent<TMP_InputField>().text;

            cells[i].transform.Find("text").gameObject.GetComponent<TMP_InputField>().text = resourceCount;
            cells[i].transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = selestedResources[i].Name;

            string path = $"{selestedResources[i].Id}";
            Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            if (sprite == null)
                cells[i].GetComponent<Image>().sprite = Resources.Load("Images/Bonus_Yell", typeof(Sprite)) as Sprite;
            else
                cells[i].GetComponent<Image>().sprite = sprite;
            cells[i].SetActive(true);

            prices.transform.SetParent(showWindow.transform);
        }
    }
    public void СopyСlipboardChest()
    {
        clipboardChest = "(";
        for (int i = 0; i < this.selestedResources.Count; i++)
        {
            GameObject countResource = selectedResourcesPrefabs[i].transform.Find("countResource").gameObject;
            GameObject textArea = countResource.transform.Find("Text Area").Find("Text").gameObject;
            string count = textArea.GetComponent<TextMeshProUGUI>().text;
            clipboardChest += $"(ResourceId=\"{selestedResources[i].Id}\",Count={count})";
            if (i != selestedResources.Count - 1) clipboardChest += ",";
        }
        clipboardChest += ")";

        GUIUtility.systemCopyBuffer = clipboardChest;
    }
    public void DeleteSelectedResourses(string nameResource)
    {
        int index = -1;
        for (int i = 0; i < selestedResources.Count; i++)
        {
            if (nameResource == selestedResources[i].Name)
                index = i;
        }
        if (index == -1) return;
        selestedResources.RemoveAt(index);
        PrintingResources();
    }
    public void CopyResoursce(GameObject gameObject, GameObject countResource)
    {
        clipboardResources = "";
        GameObject nameResource = gameObject.transform.Find("nameResource").gameObject;
        string name = nameResource.GetComponent<TextMeshProUGUI>().text;

        GameObject textArea = countResource.transform.Find("Text Area").Find("Text").gameObject;
        string count = textArea.GetComponent<TextMeshProUGUI>().text;

        foreach (Resource resource in GetSelectedChest())
        {
            if (name == resource.Name)
            {
                clipboardResources = $"(ResourceId=\"{resource.Id}\",Count={count})";
            }
        }
        GUIUtility.systemCopyBuffer = clipboardResources;
    }
    public void PastedChest()
    {

        string pasteText = GUIUtility.systemCopyBuffer;
        pasteText = pasteText.Replace("(", "{").Replace(")", "}").Replace("=", ":");
    }
    public void RenderToolTip(GameObject selectedResource, GameObject countResource, GameObject name, GameObject toolTip)
    {
        string nameString = name.GetComponent<TextMeshProUGUI>().text;
        foreach (Resource resource in selestedResources)
        {
            if (nameString == resource.Name)
            {
                int count = int.Parse(countResource.GetComponent<TMP_InputField>().text.ToString());

                GameObject priceMoney = toolTip.transform.Find("priceMoney").gameObject;
                priceMoney.SetActive(false);
                GameObject priceEnergy = toolTip.transform.Find("priceEnergy").gameObject;
                priceEnergy.SetActive(false);
                GameObject priceWater = toolTip.transform.Find("priceWater").gameObject;
                priceWater.SetActive(false);


                if (resource.PriceRelativelyMoney != 0)
                {
                    priceMoney.SetActive(true);
                    GameObject cost = priceMoney.transform.Find("cost").gameObject;
                    cost.GetComponent<TextMeshProUGUI>().text = (count * resource.PriceRelativelyMoney).ToString();
                }
                if (resource.PriceRelativelyEnergy != 0)
                {
                    priceEnergy.SetActive(true);
                    GameObject costP = priceEnergy.transform.Find("cost").gameObject;
                    costP.GetComponent<TextMeshProUGUI>().text = (count * resource.PriceRelativelyEnergy).ToString();
                }
                if (resource.PriceRelativelyWater != 0)
                {
                    priceWater.SetActive(true);
                    GameObject costS = priceWater.transform.Find("cost").gameObject;
                    costS.GetComponent<TextMeshProUGUI>().text = (count * resource.PriceRelativelyWater).ToString();
                }
            }
        }
    }
    public void CalculatingCostCheast()
    {
        int priceMoneyInt = 0;
        int priceEnergyInt = 0;
        int priceWaterInt = 0;
        for (int i = 0; i < selestedResources.Count; i++)
        {
            GameObject countResource = selectedResourcesPrefabs[i].transform.Find("countResource").gameObject;
            int resourceCount = int.Parse(countResource.GetComponent<TMP_InputField>().text);
            priceMoneyInt += (selestedResources[i].PriceRelativelyMoney * resourceCount);
            priceEnergyInt += (selestedResources[i].PriceRelativelyEnergy * resourceCount);
            priceWaterInt += (selestedResources[i].PriceRelativelyWater * resourceCount);
        }
        prices[0].transform.Find("cost").gameObject.GetComponent<TextMeshProUGUI>().text = priceMoneyInt.ToString();
        prices[1].transform.Find("cost").gameObject.GetComponent<TextMeshProUGUI>().text = priceEnergyInt.ToString();
        prices[2].transform.Find("cost").gameObject.GetComponent<TextMeshProUGUI>().text = priceWaterInt.ToString();
        foreach (GameObject price in prices)
        {
            price.SetActive(true);
        }
    }
    public void RefreshResoursce(GameObject gameObject)
    {
        GameObject selectedResourceRefresh = gameObject.transform.parent.gameObject;

        GameObject infoResource = selectedResourceRefresh.transform.Find("infoResource").gameObject;

        GameObject countResource = selectedResourceRefresh.transform.Find("countResource").gameObject;

        GameObject nameResource = infoResource.transform.Find("nameResource").gameObject;
        string name = nameResource.GetComponent<TextMeshProUGUI>().text;

        GameObject iconResource = infoResource.transform.Find("iconResource").gameObject;
        Color infoResourceColor = new Color();

        for (int i = 0; i < selestedResources.Count; i++)
        {
            if (selestedResources[i].Name == name)
            {
                Resource resource = RandomResource();

                for (int j = 0; j < selestedResources.Count; j++)
                {
                    if (selestedResources[j].Name == resource.Name)
                    {
                        resource = RandomResource();
                        j = -1;
                    }
                }

                if (resource.Level == 1) infoResourceColor = Color.white;
                if (resource.Level == 2) infoResourceColor = Color.yellow;
                if (resource.Level == 3) infoResourceColor = Color.green;
                if (resource.Level == 4) infoResourceColor = Color.red;
                infoResource.GetComponent<Image>().color = infoResourceColor;

                string path = $"{resource.Id}";
                Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                if (sprite == null)
                    iconResource.GetComponent<Image>().sprite = Resources.Load("Images/Bonus_Yell", typeof(Sprite)) as Sprite;
                else
                    iconResource.GetComponent<Image>().sprite = sprite;

                countResource.GetComponent<TMP_InputField>().text = "1";
                nameResource.GetComponent<TextMeshProUGUI>().text = resource.Name;

                selestedResources[i] = resource;
            }
        }
    }

    public void SetCountResource(string name, int count)
    {
        foreach (Resource resource in selestedResources)
        {
            if (resource.Name == name)
                resource.Count = count;
        }
    }
    private Resource RandomResource()
    {
        System.Random rand = new System.Random();
        return GetSelectedChest()[rand.Next(0, GetSelectedChest().Count)];
    }
    private int GetCountResourcesInChest()
    {
        int countResourcesInChest = 1;
        foreach (GameObject gameObject in this.togglesCountResources)
        {
            Toggle toggle = gameObject.GetComponent<Toggle>();
            if (toggle.isOn)
            {
                string text = gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>().text;
                countResourcesInChest = Convert.ToInt32(text);
            }
        }
        return countResourcesInChest;
    }
    private List<Resource> GetSelectedChest()
    {
        List<Resource> select = this.rootResources.ResourcesT1;
        foreach (GameObject gameObject in togglesSelectedChest)
        {
            Toggle toggle = gameObject.GetComponent<Toggle>();
            if (toggle.isOn)
            {
                select = gameObject.name switch
                {
                    "T1" => this.rootResources.ResourcesT1,
                    "T2" => this.rootResources.ResourcesT2,
                    "T3" => this.rootResources.ResourcesT3,
                    "T4" => this.rootResources.ResourcesT4,
                    "T5" => this.rootResources.ResourcesT5,
                    _ => this.rootResources.ResourcesT1,
                };
            }
        }
        return select;
    }
    private int[] RandomResource(int countResourses, int allResourses)
    {
        System.Random rand = new System.Random();
        int[] numbers = new int[countResourses];
        numbers[0] = rand.Next(0, allResourses);
        for (int i = 1; i < numbers.Length;)
        {
            int num = rand.Next(0, allResourses);
            int j;
            for (j = 0; j < i; j++)
            {
                if (num == numbers[j]) break;
            }
            if (j == i)
            {
                numbers[i] = num;
                i++;
            }
        }
        return numbers;
    }
    private void SetSelestedResources(int[] selectedNumberResources)
    {
        this.selestedResources.Clear();

        for (int i = 0; i < selectedNumberResources.Length; i++)
        {
            this.selestedResources.Add(GetSelectedChest()[selectedNumberResources[i]]);
        }
        foreach (Resource resource in selestedResources)
        {
            resource.Count = 1;
        }
    }
    private void PrintingResources()
    {
        foreach (GameObject gameObject in selectedResourcesPrefabs)
        {
            gameObject.SetActive(false);
        }
        for (int i = 0; i < this.selestedResources.Count; i++)
        {
            selectedResourcesPrefabs[i].SetActive(true);
            GameObject infoResource = selectedResourcesPrefabs[i].transform.Find("infoResource").gameObject;
            GameObject nameResource = infoResource.transform.Find("nameResource").gameObject;
            GameObject iconResource = infoResource.transform.Find("iconResource").gameObject;
            Color infoResourceColor = new Color();
            nameResource.GetComponent<TextMeshProUGUI>().text = selestedResources[i].Name;

            if (selestedResources[i].Level == 1) infoResourceColor = Color.white;
            if (selestedResources[i].Level == 2) infoResourceColor = Color.yellow;
            if (selestedResources[i].Level == 3) infoResourceColor = Color.green;
            if (selestedResources[i].Level == 4) infoResourceColor = Color.red;
            infoResource.GetComponent<Image>().color = infoResourceColor;



            string path = $"{selestedResources[i].Id}";
            Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            if (sprite == null)
                iconResource.GetComponent<Image>().sprite = Resources.Load("Images/Bonus_Yell", typeof(Sprite)) as Sprite;
            else
                iconResource.GetComponent<Image>().sprite = sprite;

            GameObject countResource = selectedResourcesPrefabs[i].transform.Find("countResource").gameObject;
            countResource.GetComponent<TMP_InputField>().text = selestedResources[i].Count.ToString();

        }
    }
    private void CheckingResources()
    {
        for (int j = 0; j < fixedResources.Count; j++)
        {
            GameObject dropdown = fixedResources[j].transform.Find("Dropdown").gameObject;
            GameObject label = dropdown.transform.Find("Label").gameObject;
            string nameFixRes = label.GetComponent<TextMeshProUGUI>().text;
            if (nameFixRes == "none") return;

            int index = 0;
            for (int i = 0; i < selestedResources.Count; i++)
            {
                if (selestedResources[i].Name == nameFixRes) break;
                else index++;
            }

            if (index == selestedResources.Count)
            {
                selestedResources.Remove(selestedResources[0]);

                foreach (Resource item in GetSelectedChest())
                {
                    if (item.Name == nameFixRes) selestedResources.Add(item);
                }
            }
        }
        PrintingResources();
    }


}
