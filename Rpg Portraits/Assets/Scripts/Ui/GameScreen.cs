using System.Collections.Generic;
using RpgPortraits.ScriptableObjects;
using UnityEngine;

namespace RpgPortraits.Ui
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject portraitPrefab;
        [SerializeField] private Transform content;

        [Header("Portraits To Populate")] 
        [SerializeField] private PortraitsListing portraitsListing;

        private List<DraggablePortrait> _portraits;

        private const int MAX_PORTRAITS_COUNT = 4;

        private void Start()
        {
            _portraits = new List<DraggablePortrait>();
            InitializePortraits();
        }

        private void InitializePortraits()
        {
            if (portraitsListing == null)
            {
                Debug.LogError("Portraits Listing Required!");
                return;
            }
            
            // initialize max 4 portraits even if there are more in the list
            for (int i = 0; i < MAX_PORTRAITS_COUNT; i++)
            {
                GameObject obj = Instantiate(portraitPrefab.gameObject, content);
                obj.name = (i + 1).ToString();
                DraggablePortrait draggablePortrait = obj.GetComponentInChildren<DraggablePortrait>();
                draggablePortrait.Init(portraitsListing.CharacterPortraits[i].Sprite);
                _portraits.Add(draggablePortrait);
            }
        }
    }
}