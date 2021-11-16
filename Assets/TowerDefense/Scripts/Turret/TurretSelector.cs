using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Selection = TowerDefense.TurretHandlerData.TurretSelection;

namespace TowerDefense.Turrets
{
    /// <summary>
    /// contains turret UI information and drag behavior for spawning
    /// </summary>
    public class TurretSelector : MonoBehaviour, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Button _selectorButton;

        private bool _pointerDown;

        private bool _needsRelease;

        private Selection _selection;

        private Text _priceText;

        private void Awake()
        {
            _selectorButton = GetComponent<Button>();
            _priceText = GetComponentInChildren<Text>();
        }

        public void Setup(Selection turretSelection)
        {
            name = turretSelection.name;
            var image = _selectorButton.targetGraphic as Image;
            if (image != null)
                image.sprite = turretSelection.sprite;

            _priceText.text = turretSelection.price.ToString();
            _selection = turretSelection;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (GameManager.Instance.IsGameOver)
                return;
            
            if (!_pointerDown || _needsRelease)
                return;
            _needsRelease = true;
            TurretHandler.Instance.GeneratePreviewTurret(_selection.turret, _selection.price, eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerDown = false;
            _needsRelease = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
        }
    }
}