using UnityEngine;
using UnityEngine.EventSystems;

namespace RpgPortraits.Ui.Utils
{
    public class DraggableUi : MonoBehaviour, IDragHandler
    {
        protected RectTransform RectTransform;

        private Vector3 _resultPosition;
        private Canvas _canvas;
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            _canvas = transform.root.GetComponent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }
}