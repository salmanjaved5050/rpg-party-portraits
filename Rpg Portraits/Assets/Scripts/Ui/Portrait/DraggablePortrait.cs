using DG.Tweening;
using RpgPortraits.Ui.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RpgPortraits.Ui.Portrait
{
    [RequireComponent(typeof(Image))]
    public class DraggablePortrait : DraggableUi, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private Image characterPortraitImage;
        
        private Vector3 _fallBackPosition;
        internal void Init(Sprite sprite, Vector3 positionOnUi)
        {
            characterPortraitImage.sprite = sprite;
            _fallBackPosition = positionOnUi;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            RectTransform.DOMove(_fallBackPosition, 0.5f)
                .SetEase(Ease.InOutQuad);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransform.SetAsLastSibling();
        }
    }
}