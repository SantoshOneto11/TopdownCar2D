///<summary>
/// Objective 
/// Change Sprites when fired in UI.
/// Change Sprites When Reload.
/// </summary>

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ShootBottle
{
    public class Magzine : MonoBehaviour
    {
        [SerializeField] private Image readyShellImg;

        [SerializeField] private List<Image> shellHolder;
        [SerializeField] private List<Transform> shellSpawnPoints;

        private int totalShells = 8;

        public int ActiveShells { get { return totalShells - usedShells; } }

        private int usedShells = 0;

        private void Start()
        {
            SpawnActiveShells(totalShells);
        }

        private void SpawnActiveShells(int count)
        {
            foreach (var activeShell in shellSpawnPoints)
            {
                Image obj = activeShell.AddComponent<Image>();
                shellHolder.Add(obj);
                obj.sprite = readyShellImg.sprite;
                obj.type = Image.Type.Sliced;
            }
        }

        public void ShotFired(int count)
        {


            if (usedShells + count > totalShells)
            {
                Debug.Log("No Bullets!!");
                return;
            }

            usedShells += count;

            for (int i = 0; i < usedShells; i++)
            {
                shellHolder[i].color = Color.gray;
            }

        }

        //Reload Shells to Default.
        public void ReloadShells()
        {
            usedShells = 0;

            for (int i = 0; i < totalShells; i++)
            {
                shellHolder[i].color = Color.white;
            }
        }
    }
}
