using DG.Tweening;
using RpgPortraits.Ui.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgPortraits.Ui.Portrait
{
    public class DraggablePortrait : DraggableUi, IPointerDownHandler, IPointerUpHandler
    {
        [Tooltip("Tween duration when portrait gets back to initial position or switches to a new position.")] [SerializeField]
        private float tweenBackToOriginalPositionDuration;

        [SerializeField] private Vector3 tweenScaleAmount;
        [SerializeField] private float tweenBackToOriginalScaleDuration;

        private Vector3 _fallBackPosition;
        private Image _portraitImage;

        internal void Init(Sprite sprite, Vector3 positionOnUi)
        {
            _portraitImage = GetComponentInChildren<Image>();
            _portraitImage.sprite = sprite;
            _fallBackPosition = positionOnUi;
        }

        internal void SwitchToPosition(Vector3 position)
        {
            _fallBackPosition = position;
            RectTransform.DOMove(position, tweenBackToOriginalPositionDuration)
                .SetEase(Ease.InOutQuad);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransform.SetAsLastSibling();
            transform.DOScale(tweenScaleAmount, 0.1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            RectTransform.DOMove(_fallBackPosition, tweenBackToOriginalPositionDuration)
                .SetEase(Ease.InOutQuad);
            transform.DOScale(Vector3.one, tweenBackToOriginalScaleDuration);
        }
    }
}