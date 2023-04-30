using System.Collections.Generic;
using System.Linq;
using RpgPortraits.Utility;
using UnityEngine;

namespace RpgPortraits.Ui.Portrait
{
    public class PortraitController : MonoBehaviour
    {
        [Header("Portrait Settings")] [SerializeField]
        private float offsetFromLeft;

        [SerializeField] private float spacingOffset;

        [Header("Portraits Setting")] [SerializeField]
        private GameObject portraitPrefab;

        [SerializeField] private PortraitListing portraitsListing;
        [SerializeField] private PortraitDockLocation portraitDockLocation;
        [SerializeField] private DockConfigurationListing DockConfigurationListing;

        private List<DraggablePortrait> _portraits;
        private Vector2 _portraitSize;
        private PortraitDockConfiguration _portraitDockConfiguration;

        private const int MAX_PORTRAITS_COUNT = 4;

        private void Start()
        {
            _portraits = new List<DraggablePortrait>();
            _portraitSize = portraitPrefab.GetComponent<RectTransform>()
                .rect.size;
            _portraitDockConfiguration = DockConfigurationListing.Configurations.Find(config => config.PortraitDockLocation == portraitDockLocation);
            
            CreatePortraits();
        }

        private void CreatePortraits()
        {
            if (portraitsListing == null)
            {
                Debug.LogError("Portraits Listing Required!");
                return;
            }

            // initialize max 4 portraits even if there are more in the list
            for (int i = 0; i < MAX_PORTRAITS_COUNT; i++)
            {
                GameObject portraitObject = Instantiate(portraitPrefab.gameObject, transform);
                portraitObject.name = (i + 1).ToString();

                SetupPortraitAndAdjustPosition(portraitObject, i);
            }
        }

        private void SetupPortraitAndAdjustPosition(GameObject portraitObject, int portraitIndex)
        {
            RectTransform portraitRectTransform = portraitObject.GetComponent<RectTransform>();

            Vector3 newPosition = portraitRectTransform.position;
            newPosition.x += offsetFromLeft;
            newPosition.y -= portraitIndex * (_portraitSize.y + spacingOffset);

            portraitRectTransform.position = newPosition;

            DraggablePortrait draggablePortrait = portraitObject.GetComponent<DraggablePortrait>();
            draggablePortrait.Init(portraitsListing.CharacterPortraits[portraitIndex]
                .Sprite, newPosition);
            _portraits.Add(draggablePortrait);
        }
    }
}