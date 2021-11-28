using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ObjectParameters_VIEW : MonoBehaviour
{
    private GameObject contentCharacters;
    private GameObject contentStatics;
    private GameObject contentRestores;
    private GameObject contentPlants;
    private GameObject contentRentHouses;
    private GameObject contentFactories;
    private GameObject contentGates;
    private GameObject contentTreasures;
    private GameObject contentGarbages;
    private GameObject contentResources;
    private GameObject contentDecorations;
    private GameObject contentAchievementSettings;

    private ObjectTypeList objectTypeList;

    public ObjectParameters_VIEW(Dictionary<string, GameObject> prefabs)
    {
        this.contentCharacters = prefabs.ContainsKey("contentCharacters")
            ? prefabs["contentCharacters"]
            : null;
        this.contentStatics = prefabs.ContainsKey("contentStatics")
            ? prefabs["contentStatics"]
            : null;
        this.contentRestores = prefabs.ContainsKey("contentRestores")
            ? prefabs["contentRestores"]
            : null;
        this.contentPlants = prefabs.ContainsKey("contentPlants")
            ? prefabs["contentPlants"]
            : null;
        this.contentRentHouses = prefabs.ContainsKey("contentRentHouses")
            ? prefabs["contentRentHouses"]
            : null;
        this.contentFactories = prefabs.ContainsKey("contentFactories")
            ? prefabs["contentFactories"]
            : null;
        this.contentGates = prefabs.ContainsKey("contentGates")
            ? prefabs["contentGates"]
            : null;
        this.contentTreasures = prefabs.ContainsKey("contentTreasures")
            ? prefabs["contentTreasures"]
            : null;
        this.contentGarbages = prefabs.ContainsKey("contentGarbages")
            ? prefabs["contentGarbages"]
            : null;
        this.contentResources = prefabs.ContainsKey("contentResources")
            ? prefabs["contentResources"]
            : null;
        this.contentDecorations = prefabs.ContainsKey("contentDecorations")
            ? prefabs["contentDecorations"]
            : null;
        this.contentAchievementSettings = prefabs.ContainsKey("contentAchievementSettings")
            ? prefabs["contentAchievementSettings"]
            : null;

        TextAsset objectTypeListPath = Resources.Load<TextAsset>("Objects");
        if (objectTypeListPath.text != "")
            objectTypeList = JsonConvert.DeserializeObject<ObjectTypeList>(objectTypeListPath.text);
        else
            objectTypeList = new ObjectTypeList();
        WriteObject();
    }

    public void StartRender()
    {
        if (objectTypeList.ResourcesGame == null) return;
        FillCard(objectTypeList.ResourcesGame);
    }

    public void AddCardInDeck(ClassObject classObject)
    {
        FillClassObjectAndParent(createCard(), classObject);

        WriteObject();
    }

    private void WriteObject()
    {
        File.WriteAllText(@"Assets/Resources/Objects.json", JsonConvert.SerializeObject(objectTypeList));

        using (StreamWriter file = File.CreateText(@"Assets/Resources/Objects.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, objectTypeList);
        }
    }

    private GameObject createCard()
    {
        string formFillingPrefabPath = "Prefabs/FormFilling";

        return MonoBehaviour.Instantiate(Resources.Load<GameObject>(formFillingPrefabPath)) as GameObject;
    }
    private void FillClassObjectAndParent(GameObject template, ClassObject classObject)
    {
        TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();

        switch (classObject)
        {
            case ClassObject.Character:

                template.transform.SetParent(this.contentCharacters.transform, false);
                dropdown.value = 0;
                break;

            case ClassObject.Static:

                template.transform.SetParent(this.contentStatics.transform, false);
                dropdown.value = 1;
                break;

            case ClassObject.Restore:

                template.transform.SetParent(this.contentRestores.transform, false);
                dropdown.value = 2;
                break;

            case ClassObject.Plant:

                template.transform.SetParent(this.contentPlants.transform, false);
                dropdown.value = 3;
                break;
            case ClassObject.RentHouse:

                template.transform.SetParent(this.contentRentHouses.transform, false);
                dropdown.value = 4;
                break;

            case ClassObject.Factory:

                template.transform.SetParent(this.contentFactories.transform, false);
                dropdown.value = 5;
                break;
            case ClassObject.Gate:

                template.transform.SetParent(this.contentGates.transform, false);
                dropdown.value = 6;
                break;

            case ClassObject.Treasure:

                template.transform.SetParent(this.contentTreasures.transform, false);
                dropdown.value = 7;
                break;
            case ClassObject.Garbage:

                template.transform.SetParent(this.contentGarbages.transform, false);
                dropdown.value = 8;
                break;

            case ClassObject.Resource:

                template.transform.SetParent(this.contentResources.transform, false);
                break;
            case ClassObject.Decoration:

                template.transform.SetParent(this.contentDecorations.transform, false);
                break;

            case ClassObject.Achievement:

                template.transform.SetParent(this.contentAchievementSettings.transform, false);
                break;

            default:
                break;
        }
    }
    private void FillCard(List<ResourcesGame> objects)
    {
        foreach (ResourcesGame resource in objects)
        {
            GameObject template = createCard();

            //BuildingId
            TMP_InputField inputBuildingId = template.transform.Find("InputBuildingId").GetComponent<TMP_InputField>();
            inputBuildingId.text = resource.BuildingId;
            //Name
            TMP_InputField inputName = template.transform.Find("InputName").GetComponent<TMP_InputField>();
            inputName.text = resource.Name;
            //ShortObjectDescription
            TMP_InputField inputDescription = template.transform.Find("InputDescription").GetComponent<TMP_InputField>();
            inputDescription.text = resource.ShortObjectDescription;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;

            template.transform.SetParent(this.contentResources.transform, false);
        }
        this.contentResources.transform.Find("AddForm").SetAsLastSibling();
        this.contentResources.SetActive(true);
    }

}
