using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgPortraits.Ui.Utils
{
    public class DraggableUi : MonoBehaviour, IDragHandler
    {
        protected RectTransform RectTransform;
        protected bool DragEnabled;
        protected Vector2 DragLimitsX;
        protected Vector2 DragLimitsY;

        private Vector3 _resultPosition;
        private Canvas _canvas;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _canvas = transform.root.GetComponent<Canvas>();
            DragEnabled = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!DragEnabled)
                return;

            RectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

            Vector2 finalPos = RectTransform.anchoredPosition;
            finalPos.x = Mathf.Clamp(finalPos.x, DragLimitsX.x, DragLimitsX.y);
            finalPos.y = Mathf.Clamp(finalPos.y, DragLimitsY.x, DragLimitsY.y);

            RectTransform.anchoredPosition = finalPos;
        }
    }
}