using DG.Tweening;
using RpgPortraits.Ui.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgPortraits.Ui.Portrait
{
    public class DraggablePortrait : DraggableUi, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image portraitImage;

        [Tooltip("Tween duration when portrait gets back to initial position or switches to a new position.")] [SerializeField]
        private float tweenBackToOriginalPositionDuration;

        [SerializeField] private Vector3 tweenScaleAmount;
        [SerializeField] private float tweenBackToOriginalScaleDuration;

        private Vector3 _fallBackPosition;

        internal void Init(Sprite sprite, Vector3 positionOnUi, Vector2 dragLimitX, Vector2 dragLimitY)
        {
            portraitImage.sprite = sprite;
            _fallBackPosition = positionOnUi;
            DragLimitsX = dragLimitX;
            DragLimitsY = dragLimitY;
            Debug.Log(DragLimitsX);
            DragEnabled = true;
        }

        internal void SwitchToPosition(Vector3 position)
        {
            _fallBackPosition = position;
            RectTransform.DOMove(position, tweenBackToOriginalPositionDuration)
                .SetEase(Ease.InOutQuad);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!DragEnabled)
                return;
            
            RectTransform.SetAsLastSibling();
            transform.DOScale(tweenScaleAmount, 0.1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!DragEnabled)
                return;
            
            RectTransform.DOMove(_fallBackPosition, tweenBackToOriginalPositionDuration)
                .SetEase(Ease.InOutQuad);
            transform.DOScale(Vector3.one, tweenBackToOriginalScaleDuration);
        }
    }
}