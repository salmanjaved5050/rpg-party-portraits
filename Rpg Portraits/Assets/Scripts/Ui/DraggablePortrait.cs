using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgPortraits.Ui
{
    [RequireComponent(typeof(Image))]
    public class DraggablePortrait : DraggableUi, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private RectTransform holderRectTransform;
        [SerializeField] private float fallbackDampingSpeed;

        private RectTransform _rectTransform;
        private Image _image;
        private Vector3 _fallBackVelocity = Vector3.zero;
        private bool _fallBackToHolder;


        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_fallBackToHolder)
            {
                _rectTransform.position = Vector3.SmoothDamp(_rectTransform.position, holderRectTransform.position, ref _fallBackVelocity, fallbackDampingSpeed);
            }
        }

        internal void Init(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _fallBackToHolder = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _fallBackToHolder = false;
        }
    }
}