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

        internal void Init(Sprite sprite, Vector3 positionOnUi)
        {
            portraitImage.sprite = sprite;
            _fallBackPosition = positionOnUi;
        }

        internal void SetDragLimitsAndEnableDrag(Vector2 dragLimitX, Vector2 dragLimitY)
        {
            DragLimitsX = dragLimitX;
            DragLimitsY = dragLimitY.x < dragLimitY.y ? new Vector2(dragLimitY.x, dragLimitY.y) : new Vector2(dragLimitY.y, dragLimitY.x);
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