using System;
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

        [Header("Portraits Setting")]
        [SerializeField] private PortraitDockLocation portraitDockLocation;
        [SerializeField] private GameObject portraitPrefab;
        [SerializeField] private PortraitListing portraitsListing;
        [SerializeField] private DockConfigurationListing DockConfigurationListing;

        private List<DraggablePortrait> _portraits;
        private Vector2 _portraitSize;
        private PortraitDockConfiguration _currentDockConfiguration;
        private RectTransform _rectTransform;

        private const int MAX_PORTRAITS_COUNT = 4;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _portraits = new List<DraggablePortrait>();
            _portraitSize = portraitPrefab.GetComponent<RectTransform>()
                .rect.size;
            _currentDockConfiguration = DockConfigurationListing.Configurations.Find(config => config.PortraitDockLocation == portraitDockLocation);

            DockPortraitPanel();
        }

        private void DockPortraitPanel()
        {
            switch (portraitDockLocation)
            {
                case PortraitDockLocation.Left:
                    _rectTransform.pivot = new Vector2(0, 1);
                    _rectTransform.anchorMin = new Vector2(0, 1);
                    _rectTransform.anchorMax = new Vector2(0, 1);
                    break;
                case PortraitDockLocation.Right:
                    _rectTransform.pivot = new Vector2(1, 1);
                    _rectTransform.anchorMin = new Vector2(1, 1);
                    _rectTransform.anchorMax = new Vector2(1, 1);
                    break;
                case PortraitDockLocation.Bottom:
                    _rectTransform.pivot = new Vector2(0.5f, 0);
                    _rectTransform.anchorMin = new Vector2(0.5f, 0);
                    _rectTransform.anchorMax = new Vector2(0.5f, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Vector3 newPosition = _rectTransform.position;
            newPosition.x += _currentDockConfiguration.HorizontalDockOffset;
            newPosition.y += _currentDockConfiguration.VerticalDockOffset;

            _rectTransform.position = newPosition;
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

        private void PositionPortrait(RectTransform rectTransform, int index)
        {
            switch (portraitDockLocation)
            {
                case PortraitDockLocation.Left:
                    break;
                case PortraitDockLocation.Right:
                    break;
                case PortraitDockLocation.Bottom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}