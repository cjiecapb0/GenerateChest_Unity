public class ResourcesGame
{
    public string BuildingId { get; set; }
    public string Name { get; set; }
    public string ShortObjectDescription { get; set; }
    public string Icon { get; set; }
}
public class Achievement : ResourcesGame { }
public class Character : ResourcesGame
{
    public string BuildingClass { get; set; }
    public bool bIsBlockMove { get; set; }
    public bool bIsBlockFog { get; set; }
    public bool bIsFogHasInfluence { get; set; }
    public bool bIsMoving { get; set; }
}
public class Static : Character { }
public class Plant : Character { }
public class RentHouse : Character { }
public class Factory : Character { }
public class Gate : Character { }
public class Treasure : Character { }
public class Garbage : Character { }
public class Decoration : Character { }

