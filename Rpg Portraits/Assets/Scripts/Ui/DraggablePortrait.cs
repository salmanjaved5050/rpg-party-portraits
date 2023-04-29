using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgPortraits.Ui
{
    [RequireComponent(typeof(Image))]
    public class DraggablePortrait : DraggableUi, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private RectTransform holderRectTransform;

        private RectTransform _rectTransform;
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        internal void Init(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.DOMove(holderRectTransform.position, 0.5f)
                .SetEase(Ease.InOutQuad);
        }

        public void OnBeginDrag(PointerEventData eventData) { }
    }
}