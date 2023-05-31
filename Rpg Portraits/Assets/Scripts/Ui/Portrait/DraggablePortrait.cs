using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RpgPortraits.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgPortraits.Ui.Portrait
{
    public class DraggablePortrait : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image portraitImage;
        [SerializeField] private float tweenBackToOriginalPositionDuration;
        [SerializeField] private Vector3 tweenScaleAmount;
        [SerializeField] private float tweenBackToOriginalScaleDuration;

        private Vector3 _fallBackPosition;
        private PortraitController _portraitController;

        private RectTransform _rectTransform;
        private bool _dragEnabled;
        private Vector2 _dragLimitsX;
        private Vector2 _dragLimitsY;
        private List<DraggablePortrait> _portraits;

        private Vector3 _resultPosition;
        private Canvas _canvas;
        private int _portraitIndex;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = transform.root.GetComponent<Canvas>();
            _dragEnabled = false;
        }

        private IEnumerator LerpToPosition(float duration)
        {
            float elapsedTime = 0;
            Vector3 startPosition = _rectTransform.anchoredPosition;
            while (elapsedTime < duration)
            {
                _rectTransform.anchoredPosition = Vector3.Slerp(startPosition, _fallBackPosition, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator SwitchPosition()
        {
            yield return LerpToPosition(tweenBackToOriginalPositionDuration);
            _dragEnabled = true;
        }

        internal void Init(Sprite sprite, Vector3 positionOnUi, int portraitIndex, PortraitController portraitController)
        {
            portraitImage.sprite = sprite;
            _fallBackPosition = positionOnUi;
            _portraitController = portraitController;
            _portraitIndex = portraitIndex;
        }

        internal void SetDragLimitsAndEnableDrag(Vector2 dragLimitX, Vector2 dragLimitY)
        {
            _dragLimitsX = dragLimitX;
            _dragLimitsY = dragLimitY.x < dragLimitY.y ? new Vector2(dragLimitY.x, dragLimitY.y) : new Vector2(dragLimitY.y, dragLimitY.x);
            _dragEnabled = true;

            _portraits = _portraitController.GetPortraits();
        }

        internal void SwitchToPosition(Vector3 position)
        {
            _fallBackPosition = position;
            transform.DOScale(Vector3.one, tweenBackToOriginalScaleDuration);
            StartCoroutine(SwitchPosition());
        }

        internal Vector3 GetFallBackPosition()
        {
            return _fallBackPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_dragEnabled)
                return;

            _rectTransform.SetAsLastSibling();
            transform.DOScale(tweenScaleAmount, 0.1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_dragEnabled)
                return;

            StartCoroutine(LerpToPosition(tweenBackToOriginalPositionDuration));
            transform.DOScale(Vector3.one, tweenBackToOriginalScaleDuration);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_dragEnabled)
                return;

            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

            Vector2 finalPos = _rectTransform.anchoredPosition;
            finalPos.x = Mathf.Clamp(finalPos.x, _dragLimitsX.x, _dragLimitsX.y);
            finalPos.y = Mathf.Clamp(finalPos.y, _dragLimitsY.x, _dragLimitsY.y);

            _rectTransform.anchoredPosition = finalPos;


            // check if this portrait overlaps with others
            for (int i = 0; i < _portraits.Count; i++)
            {
                if (i == _portraitIndex)
                    continue;

                if (GameUtils.Overlaps(_rectTransform, _portraits[i]
                        .GetComponent<RectTransform>()))
                {
                    _dragEnabled = false;
                    _portraitController.SwitchPortraits(_portraitIndex, i);
                }
            }
        }
    }
}