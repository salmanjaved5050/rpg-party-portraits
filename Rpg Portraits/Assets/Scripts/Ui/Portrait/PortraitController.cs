using System;
using System.Collections.Generic;
using RpgPortraits.Utility;
using UnityEngine;

namespace RpgPortraits.Ui.Portrait
{
    public class PortraitController : MonoBehaviour
    {
        [Header("Portraits Setting")] [SerializeField]
        private PortraitDockLocation dockLocation;

        [SerializeField] private GameObject portraitPrefab;
        [SerializeField] private List<CharacterPortrait> characterPortraits;
        [SerializeField] private DockConfigurationListing DockConfigurationListing;

        private List<DraggablePortrait> _portraits;
        private Vector2 _portraitSize;
        private PortraitDockConfiguration _currentDockConfiguration;
        private RectTransform _rectTransform;
        private Vector2 _dragLimitX;
        private Vector2 _dragLimitY;

        private const int DRAG_X_LIMIT = 15;
        private const int DRAG_Y_LIMIT = 15;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _portraits = new List<DraggablePortrait>();
            _portraitSize = portraitPrefab.GetComponent<RectTransform>()
                .rect.size;
            _currentDockConfiguration = DockConfigurationListing.Configurations.Find(config => config.PortraitDockLocation == dockLocation);

            DockPortraitPanel();
            CreatePortraits();
        }

        private void DockPortraitPanel()
        {
            switch (dockLocation)
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
                case PortraitDockLocation.Top:
                    _rectTransform.pivot = new Vector2(0.5f, 1);
                    _rectTransform.anchorMin = new Vector2(0.5f, 1);
                    _rectTransform.anchorMax = new Vector2(0.5f, 1);
                    break;
                case PortraitDockLocation.Bottom:
                    _rectTransform.pivot = new Vector2(0.5f, 0);
                    _rectTransform.anchorMin = new Vector2(0.5f, 0);
                    _rectTransform.anchorMax = new Vector2(0.5f, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Vector3 dockPosition = _rectTransform.position;
            dockPosition.x += _currentDockConfiguration.HorizontalDockOffset;
            dockPosition.y += _currentDockConfiguration.VerticalDockOffset;

            _rectTransform.position = dockPosition;
        }

        private void CreatePortraits()
        {
            if (characterPortraits == null)
            {
                Debug.LogError("Character Portraits Required!");
                return;
            }

            // initialize max 4 portraits even if there are more in the list
            for (int i = 0; i < characterPortraits.Count; i++)
            {
                AddPortraitToPanel(characterPortraits[i], i);
            }
        }

        private void AddPortraitToPanel(CharacterPortrait characterPortrait, int index)
        {
            GameObject portraitObject = Instantiate(portraitPrefab.gameObject, transform);
            portraitObject.name = characterPortrait.name;

            SetupPortraitsAndAdjustPositions(portraitObject, index);
        }

        private void SetupPortraitsAndAdjustPositions(GameObject portraitObject, int portraitIndex)
        {
            RectTransform portraitRectTransform = portraitObject.GetComponent<RectTransform>();

            Vector3 portraitPosition = GetDockedPortraitPosition(portraitRectTransform, portraitIndex);

            portraitRectTransform.position = portraitPosition;

            CalculateDragLimits();

            DraggablePortrait draggablePortrait = portraitObject.GetComponent<DraggablePortrait>();
            draggablePortrait.Init(characterPortraits[portraitIndex]
                .Sprite, portraitPosition, _dragLimitX, _dragLimitY);
            _portraits.Add(draggablePortrait);
        }

        private Vector3 GetDockedPortraitPosition(RectTransform rectTransform, int portraitIndex)
        {
            Vector3 position = rectTransform.position;
            switch (dockLocation)
            {
                case PortraitDockLocation.Left:
                case PortraitDockLocation.Right:
                    position.y -= portraitIndex * (_portraitSize.y + _currentDockConfiguration.SpacingBetweenPortraits);
                    break;
                case PortraitDockLocation.Top:
                case PortraitDockLocation.Bottom:
                    position.x += portraitIndex * (_portraitSize.x + _currentDockConfiguration.SpacingBetweenPortraits);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return position;
        }

        private void CalculateDragLimits()
        {
            Rect rect = portraitPrefab.GetComponent<RectTransform>()
                .rect;
            switch (dockLocation)
            {
                case PortraitDockLocation.Left:
                case PortraitDockLocation.Right:
                    _dragLimitX = new Vector2(rect.center.x - DRAG_X_LIMIT, rect.center.x + DRAG_X_LIMIT);
                    break;
                case PortraitDockLocation.Top:
                case PortraitDockLocation.Bottom:
                    _dragLimitY = new Vector2(rect.center.y - DRAG_Y_LIMIT, rect.center.y + DRAG_Y_LIMIT);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}