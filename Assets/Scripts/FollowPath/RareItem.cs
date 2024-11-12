using UnityEngine;
namespace ShootBottle
{
    [CreateAssetMenu(fileName = "RareItem", menuName = "Game/RareItem", order = 1)]
    public class RareItem : ScriptableObject
    {
        public string itemName;
        public int rarityLevel;
        public int unlockLevel;
    }
}
