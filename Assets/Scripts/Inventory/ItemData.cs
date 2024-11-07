using UnityEngine;

[CreateAssetMenu(menuName = "CustomData/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject model;
    [TextArea]
    public string description;
}
