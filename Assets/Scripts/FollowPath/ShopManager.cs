using System.Collections.Generic;
using UnityEngine;

namespace ShootBottle
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] List<ShopItem> product;

        private int totalProduct;
        private void Start()
        {
            totalProduct = product.Count;
        }

        public void SetupShopData(List<ShopItem> items)
        {
            foreach (var item in items)
            {
                string name = item.Name.text;
            }
        }
    }
}
