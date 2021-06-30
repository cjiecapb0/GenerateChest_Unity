using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Linq;

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
    private System.Random random;

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

        random = new System.Random();
        TextAsset rootResourcesPath = Resources.Load<TextAsset>("data");
        rootResources = JsonConvert.DeserializeObject<RootResources>(rootResourcesPath.text);
        selestedResources = new List<Resource>();
        clipboardChest = "";
        clipboardResources = "";
    }
    public void FillFixedResources()
    {
        List<Resource> resources = GetSelectedChest();
        foreach (TMP_Dropdown dropdown in from GameObject gameObject in this.fixedResources
                                          let gameObjectDropdown = gameObject.transform.Find("Dropdown").gameObject
                                          let dropdown = gameObjectDropdown.GetComponent<TMP_Dropdown>()
                                          select dropdown)
        {
            dropdown.options.Clear();
            dropdown.options.Add(new TMP_Dropdown.OptionData { text = "none" });
            dropdown.transform.Find("Label").gameObject.GetComponent<TextMeshProUGUI>().text = "none";
            dropdown.options.AddRange(from Resource resource in resources
                                      select new TMP_Dropdown.OptionData { text = resource.Name });
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

            if (nameFixRes == "none") PrintingResources();
            else CheckingResources();

            CalculatingCostCheast();
        }
    }
    public void CopyResoursce(string nameResource, string countResource)
    {
        clipboardResources = "";
        foreach (Resource resource in from Resource resource in GetSelectedChest()
                                      where nameResource == resource.Name
                                      select resource)
        {
            clipboardResources = $"(ResourceId=\"{resource.Id}\",Count={countResource})";
        }

        GUIUtility.systemCopyBuffer = clipboardResources;
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
    public void RenderToolTip(string countResource, string nameResource, GameObject toolTip)
    {
        foreach (Resource resource in selestedResources)
        {
            if (nameResource == resource.Name)
            {
                int count = int.Parse(countResource);

                GameObject priceMoney = toolTip.transform.Find("priceMoney").gameObject;
                priceMoney.SetActive(false);
                if (resource.PriceRelativelyMoney != 0)
                {
                    priceMoney.SetActive(true);
                    GameObject cost = priceMoney.transform.Find("cost").gameObject;
                    cost.GetComponent<TextMeshProUGUI>().text = (count * resource.PriceRelativelyMoney).ToString();
                }

                GameObject priceEnergy = toolTip.transform.Find("priceEnergy").gameObject;
                priceEnergy.SetActive(false);
                if (resource.PriceRelativelyEnergy != 0)
                {
                    priceEnergy.SetActive(true);
                    GameObject costP = priceEnergy.transform.Find("cost").gameObject;
                    costP.GetComponent<TextMeshProUGUI>().text = (count * resource.PriceRelativelyEnergy).ToString();
                }

                GameObject priceWater = toolTip.transform.Find("priceWater").gameObject;
                priceWater.SetActive(false);
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
    public void RefreshResoursce(GameObject gameObjectResource)
    {
        GameObject selectedResourceRefresh = gameObjectResource.transform.parent.gameObject;
        GameObject infoResource = selectedResourceRefresh.transform.Find("infoResource").gameObject;
        GameObject countResource = selectedResourceRefresh.transform.Find("countResource").gameObject;
        GameObject nameResource = infoResource.transform.Find("nameResource").gameObject;
        GameObject iconResource = infoResource.transform.Find("iconResource").gameObject;

        string name = nameResource.GetComponent<TextMeshProUGUI>().text;



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

                infoResource.GetComponent<Image>().color = GetColorResource(resource.Level);
                iconResource.GetComponent<Image>().sprite = FillIconResource(resource.Id);
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

            cells[i].transform.Find("count").gameObject.GetComponent<TMP_InputField>().text = resourceCount;
            cells[i].transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = selestedResources[i].Name;

            cells[i].GetComponent<Image>().sprite = FillIconResource(selestedResources[i].Id);

            cells[i].SetActive(true);
            prices.transform.SetParent(showWindow.transform);
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
            GameObject countResource = selectedResourcesPrefabs[i].transform.Find("countResource").gameObject;
            GameObject nameResource = infoResource.transform.Find("nameResource").gameObject;
            GameObject iconResource = infoResource.transform.Find("iconResource").gameObject;

            nameResource.GetComponent<TextMeshProUGUI>().text = selestedResources[i].Name;
            infoResource.GetComponent<Image>().color = GetColorResource(selestedResources[i].Level);
            iconResource.GetComponent<Image>().sprite = FillIconResource(selestedResources[i].Id);
            countResource.GetComponent<TMP_InputField>().text = selestedResources[i].Count.ToString();
        }
    }
    private Resource RandomResource()
    {
        return GetSelectedChest()[random.Next(0, GetSelectedChest().Count)];
    }
    private int[] RandomResource(int countResourses, int countAllResourses)
    {
        int[] numbers = new int[countResourses];
        numbers[0] = random.Next(0, countAllResourses);
        for (int i = 1; i < numbers.Length;)
        {
            int num = random.Next(0, countAllResourses);
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
    private Sprite FillIconResource(string idResource)
    {
        string path = $"IconResource/{idResource}";
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        if (sprite == null)
            return Resources.Load("Images/Bonus_Yell", typeof(Sprite)) as Sprite;
        else
            return sprite;
    }
    private List<Resource> GetSelectedChest()
    {
        List<Resource> select = rootResources.ResourcesT1;
        foreach (GameObject gameObject in togglesSelectedChest)
        {
            Toggle toggle = gameObject.GetComponent<Toggle>();
            if (toggle.isOn)
            {
                select = gameObject.name switch
                {
                    "T1" => rootResources.ResourcesT1,
                    "T2" => rootResources.ResourcesT2,
                    "T3" => rootResources.ResourcesT3,
                    "T4" => rootResources.ResourcesT4,
                    "T5" => rootResources.ResourcesT5,
                    _ => rootResources.ResourcesT1,
                };
            }
        }
        return select;
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
    private Color GetColorResource(int level)
    {
        Color infoResourceColor = new Color();
        if (level == 1) infoResourceColor = Color.white;
        if (level == 2) infoResourceColor = Color.yellow;
        if (level == 3) infoResourceColor = Color.green;
        if (level == 4) infoResourceColor = Color.red;
        return infoResourceColor;
    }


}
