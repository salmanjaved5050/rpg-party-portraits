using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgPortraits.Ui.Utils
{
    public class DraggableUi : MonoBehaviour, IDragHandler
    {
        [SerializeField] private float dragDampingSpeed;
        [SerializeField] private RectTransform rectTransform;

        private Vector3 _resultPosition;
        private Vector3 _dragVelocity = Vector3.zero;

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera,
                    out _resultPosition))
            {
                rectTransform.position = Vector3.SmoothDamp(rectTransform.position, _resultPosition, ref _dragVelocity, dragDampingSpeed);
            }
        }
    }
}