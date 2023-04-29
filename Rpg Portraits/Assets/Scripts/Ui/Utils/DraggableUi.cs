using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgPortraits.Ui.Utils
{
    public class DraggableUi : MonoBehaviour, IDragHandler
    {
        [SerializeField] private float dragDampingSpeed;
        
        private Vector3 _resultPosition;
        private Vector3 _dragVelocity = Vector3.zero;
        
        protected RectTransform RectTransform;

        private void Start()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(RectTransform, eventData.position, eventData.pressEventCamera,
                    out _resultPosition))
            {
                RectTransform.position = Vector3.SmoothDamp(RectTransform.position, _resultPosition, ref _dragVelocity, dragDampingSpeed);
            }
        }
    }
}