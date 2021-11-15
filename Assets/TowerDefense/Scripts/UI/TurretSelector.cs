using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Selection = TowerDefense.TurretHandlerData.TurretSelection;


namespace TowerDefense.Turrets
{
    public class TurretSelector : MonoBehaviour, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Button _selectorImage;

        private bool _pointerDown;

        private Turret turretPrefab;
        
        private void Awake()
        {
            _selectorImage = GetComponent<Button>();
        }

        public void Setup(Selection turretSelection)
        {
            name = turretSelection.name;
            var image = _selectorImage.targetGraphic as Image;
            if (image != null)
                image.sprite = turretSelection.sprite;
 
            turretPrefab = turretSelection.turret; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_pointerDown)
                return;

            TurretHandler.Instance.GeneratePreviewTurret(turretPrefab, eventData.position);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerDown = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
        }
    }
}