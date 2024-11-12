using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootBottle
{
    [Serializable]
    public class ShopItem : MonoBehaviour
    {
        public TMP_Text Name;

        public Button Buy;
        public TMP_Text Price;
    }
}
