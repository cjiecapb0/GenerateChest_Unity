using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        {
            objectTypeList = new ObjectTypeList();
            WriteObject();
        }
    }

    public void StartRender()
    {
        //Characters
        if (objectTypeList.Characters == null) return;
        FillCard(objectTypeList.Characters);
        //Statics
        if (objectTypeList.Statics == null) return;
        FillCard(objectTypeList.Statics);
        //Restore
        if (objectTypeList.Restores == null) return;
        FillCard(objectTypeList.Restores);
        //Plants
        if (objectTypeList.Plants == null) return;
        FillCard(objectTypeList.Plants);
        //RentHouses
        if (objectTypeList.RentHouses == null) return;
        FillCard(objectTypeList.RentHouses);
        //Factories
        if (objectTypeList.Factores == null) return;
        FillCard(objectTypeList.Factores);
        //Gates
        if (objectTypeList.Gates == null) return;
        FillCard(objectTypeList.Gates);
        //Treasures
        if (objectTypeList.Treasures == null) return;
        FillCard(objectTypeList.Treasures);
        //Garbages
        if (objectTypeList.Garbages == null) return;
        FillCard(objectTypeList.Garbages);
        //ResourcesGame
        if (objectTypeList.ResourcesGame == null) return;
        FillCard(objectTypeList.ResourcesGame);
        //Decorations
        if (objectTypeList.Decorations == null) return;
        FillCard(objectTypeList.Decorations);
        //Achievement
        if (objectTypeList.Achievements == null) return;
        FillCard(objectTypeList.Achievements);
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

    private void FillCard(List<Character> objects)
    {
        foreach (Character resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentCharacters.transform, false);
        }
        this.contentCharacters.transform.Find("AddForm").SetAsLastSibling();
        this.contentCharacters.SetActive(true);
    }
    private void FillCard(List<Static> objects)
    {
        foreach (Character resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentStatics.transform, false);
        }
        this.contentStatics.transform.Find("AddForm").SetAsLastSibling();
        this.contentStatics.SetActive(true);
    }
    private void FillCard(List<Restore> objects)
    {
        foreach (Restore resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentRestores.transform, false);
        }
        this.contentRestores.transform.Find("AddForm").SetAsLastSibling();
        this.contentRestores.SetActive(true);
    }
    private void FillCard(List<Plant> objects)
    {
        foreach (Plant resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentPlants.transform, false);
        }
        this.contentPlants.transform.Find("AddForm").SetAsLastSibling();
        this.contentPlants.SetActive(true);
    }
    private void FillCard(List<RentHouse> objects)
    {
        foreach (RentHouse resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentRentHouses.transform, false);
        }
        this.contentRentHouses.transform.Find("AddForm").SetAsLastSibling();
        this.contentRentHouses.SetActive(true);
    }
    private void FillCard(List<Factory> objects)
    {
        foreach (Factory resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentFactories.transform, false);
        }
        this.contentFactories.transform.Find("AddForm").SetAsLastSibling();
        this.contentFactories.SetActive(true);
    }
    private void FillCard(List<Gate> objects)
    {
        foreach (Gate resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentGates.transform, false);
        }
        this.contentGates.transform.Find("AddForm").SetAsLastSibling();
        this.contentGates.SetActive(true);
    }
    private void FillCard(List<Treasure> objects)
    {
        foreach (Treasure resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentTreasures.transform, false);
        }
        this.contentTreasures.transform.Find("AddForm").SetAsLastSibling();
        this.contentTreasures.SetActive(true);
    }
    private void FillCard(List<Garbage> objects)
    {
        foreach (Garbage resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentGarbages.transform, false);
        }
        this.contentGarbages.transform.Find("AddForm").SetAsLastSibling();
        this.contentGarbages.SetActive(true);
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
    private void FillCard(List<Decoration> objects)
    {
        foreach (Decoration resource in objects)
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
            //BuildingClass
            TMP_Dropdown dropdown = template.transform.Find("DropdownClass").GetComponent<TMP_Dropdown>();
            dropdown.value = 0;
            //Icon
            TMP_InputField inputIcon = template.transform.Find("InputIcon").GetComponent<TMP_InputField>();
            inputIcon.text = resource.Icon;
            //BlockMove
            Toggle toggleBlockMove = template.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
            toggleBlockMove.isOn = true;
            //BlockFog
            Toggle toggleBlockFog = template.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
            toggleBlockFog.isOn = false;
            //FogHasInfluence
            Toggle toggleFogHasInfluence = template.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
            toggleFogHasInfluence.isOn = true;
            //BlockFog
            Toggle toggleMoving = template.transform.Find("ToggleMoving").GetComponent<Toggle>();
            toggleMoving.isOn = false;

            template.transform.SetParent(this.contentDecorations.transform, false);
        }
        this.contentDecorations.transform.Find("AddForm").SetAsLastSibling();
        this.contentDecorations.SetActive(true);
    }
    private void FillCard(List<Achievement> objects)
    {
        foreach (Achievement resource in objects)
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

            template.transform.SetParent(this.contentAchievementSettings.transform, false);
        }
        this.contentAchievementSettings.transform.Find("AddForm").SetAsLastSibling();
        this.contentAchievementSettings.SetActive(true);
    }

    private void RecordingCard(GameObject card, ClassObject classObject)
    {
        switch (classObject)
        {
            case ClassObject.Character:

                Character character = new Character();

                //BuildingId
                RecordingBuildingId(card, character);
                //Name
                RecordingName(card, character);
                //ShortObjectDescription
                RecordingDescription(card, character);
                //BuildingClass
                character.BuildingClass = CONSTANT.buildingClassCharacters;
                //Icon
                TMP_InputField inputIcon = card.transform.Find("InputIcon").GetComponent<TMP_InputField>();
                character.Icon = inputIcon.text;
                //BlockMove
                Toggle toggleBlockMove = card.transform.Find("ToggleBlockMove").GetComponent<Toggle>();
                character.bIsBlockMove =  toggleBlockMove.isOn;
                //BlockFog
                Toggle toggleBlockFog = card.transform.Find("ToggleBlockFog").GetComponent<Toggle>();
                character.bIsBlockFog = toggleBlockFog.isOn;
                //FogHasInfluence
                Toggle toggleFogHasInfluence = card.transform.Find("ToggleFogHasInfluence").GetComponent<Toggle>();
                character.bIsFogHasInfluence = toggleFogHasInfluence.isOn;
                //BlockFog
                Toggle toggleMoving = card.transform.Find("ToggleMoving").GetComponent<Toggle>();
                toggleMoving.isOn = false;
                break;
            case ClassObject.Static:
                break;
            case ClassObject.Restore:
                break;
            case ClassObject.Plant:
                break;
            case ClassObject.RentHouse:
                break;
            case ClassObject.Factory:
                break;
            case ClassObject.Gate:
                break;
            case ClassObject.Treasure:
                break;
            case ClassObject.Garbage:
                break;
            case ClassObject.Resource:
                break;
            case ClassObject.Decoration:
                break;
            case ClassObject.Achievement:
                break;
            default:
                break;
        }
    }

    public void RecordingBuildingId(GameObject card, ResourcesGame classObject)
    {
        TMP_InputField inputBuildingId = card.transform.Find("InputBuildingId").GetComponent<TMP_InputField>();
        classObject.BuildingId = inputBuildingId.text;
    }
    public void RecordingName(GameObject card, ResourcesGame classObject)
    {
        TMP_InputField inputName = card.transform.Find("InputName").GetComponent<TMP_InputField>();
        classObject.Name = inputName.text;
    }
    public void RecordingDescription(GameObject card, ResourcesGame classObject)
    {
        TMP_InputField inputDescription = card.transform.Find("InputDescription").GetComponent<TMP_InputField>();
        classObject.ShortObjectDescription = inputDescription.text;
    }
}
