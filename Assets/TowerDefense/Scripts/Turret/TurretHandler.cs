using System;
using TowerDefense.Currency;
using TowerDefense.Input;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Turrets
{
    /// <summary>
    /// simple turret handler & placer
    /// </summary>
    public class TurretHandler : MonoBehaviour
    {
        /// <summary>
        /// static instance
        /// </summary>
        public static TurretHandler Instance { get; private set; }

        /// <summary>
        /// layers to check for preview layers
        /// </summary>
        public LayerMask raycastPreviewLayers = 1 << 9;

        /// <summary>
        /// layers to check for placing
        /// </summary>
        public LayerMask overlapLayers;

        /// <summary>
        /// collision identifier material
        /// </summary>
        public Material collisionMaterial;

        /// <summary>
        /// cached camera reference
        /// </summary>
        private Camera _placerCamera;
        
        /// <summary>
        /// obstacles that block the turret placing 
        /// </summary>
        private readonly Collider[] _obstacleOverlap = new Collider[1];

        /// <summary>
        /// reference of turret to place
        /// </summary>
        private Turret _selectedTurret;

        /// <summary>
        /// turret to place price 
        /// </summary>
        private float _selectedPrice;

        /// <summary>
        /// preview approved placement color
        /// </summary>
        public Color acceptedPlacement;
        
        /// <summary>
        /// preview denied placement color
        /// </summary>
        public Color deniedPlacement;

        /// <summary>
        /// collision detection half size
        /// </summary>
        private readonly Vector3 _halfExtends = new Vector3(1.75f, 2, 1.75f);
        
        /// <summary>
        /// can place turret
        /// </summary>
        private bool _canPlace;

        public UnityEvent onTurretPreviewGenerated = new UnityEvent();

        public UnityEvent onTurretPreviewPlaced = new UnityEvent();

        public UnityEvent onTurretPreviewCancelled = new UnityEvent();

        private void Awake()
        {
            Instance = this;
            _placerCamera = Camera.main;
            GenerateCollisionIndicator();
        }

        private void Start()
        {
            GameManager.Instance.onGameOver.AddListener(OnGameOver);
            GameManager.Instance.onGamePause.AddListener(OnGamePause);
        }

        private void Update()
        {
            HandleTurretPreview();
        }

        private void HandleTurretPreview()
        {
            // preview has been generated and click has not been lifted
            if (_selectedTurret != null && InputHandler.TurretPlacer.PreviewTurret.ReadValue<float>() >= 1)
            {
                var placerPosition = InputHandler.TurretPlacer.PlacerPosition.ReadValue<Vector2>();
                var ray = _placerCamera.ScreenPointToRay(placerPosition);

                if (!Physics.Raycast(ray, out var hit, 100, raycastPreviewLayers))
                    return;

                // make turret follow mouse
                _selectedTurret.transform.position = hit.point;

                // if can't purchase, indicate so
                if (!CurrencyHandler.Instance.CanGoldPurchase(_selectedPrice)) {
                    _collisionIndicator.material.color = deniedPlacement;
                    _canPlace = false;
                    return;
                }

                // check if there are obstacles in the way
                var numOfObstacles = Physics.OverlapBoxNonAlloc(hit.point, _halfExtends, _obstacleOverlap,
                    Quaternion.identity, overlapLayers);

                if (numOfObstacles > 0)
                {
                    // not allowed
                    _collisionIndicator.material.color = deniedPlacement;
                    _canPlace = false;
                    return;
                }

                // allowed to place
                _collisionIndicator.material.color = acceptedPlacement;
                _canPlace = true;
            }

            // allowed to place and has preview, and lifted drag
            else if (_canPlace && _selectedTurret != null && InputHandler.TurretPlacer.PreviewTurret.ReadValue<float>() <= 0)
            {
                if (!CurrencyHandler.Instance.GoldPurchase(_selectedPrice))
                {
                    TurretPlacementFailed();
                    return;
                }

                TurretPlacementSucceed();
            }

            // can't place, has preview and lifted.
            else if (!_canPlace && _selectedTurret != null &&
                     InputHandler.TurretPlacer.PreviewTurret.ReadValue<float>() <= 0)
            {
                TurretPlacementFailed();
            }
        }

        /// <summary>
        /// called when placing preview turret succeeds
        /// </summary>
        private void TurretPlacementSucceed()
        {
            onTurretPreviewPlaced.Invoke();
            _canPlace = false;
            ResetCollisionIndicator();
            if (_selectedTurret != null)
                _selectedTurret.Activate();
            _selectedTurret = null;
        }

        /// <summary>
        /// called when placing preview turret fails
        /// </summary>
        private void TurretPlacementFailed()
        {
            onTurretPreviewCancelled.Invoke();
            ResetCollisionIndicator();
            if(_selectedTurret != null)
                Destroy(_selectedTurret.gameObject);
        }

        /// <summary>
        /// generates a turret and uses it for preview
        /// </summary>
        /// <param name="prefab">turret prefab</param>
        /// <param name="price">turret price</param>
        /// <param name="position">spawn position</param>
        public void GeneratePreviewTurret(Turret prefab, float price, Vector3 position)
        {
            var ray = _placerCamera.ScreenPointToRay(position);

            if (!Physics.Raycast(ray, out var hit, 100, raycastPreviewLayers))
                return;

            if (!hit.collider.tag.Equals("Battlefield"))
                return;
            _selectedPrice = price;
            _selectedTurret = Turret.Create(prefab, hit.point);
            EnableCollisionIndicator();
            onTurretPreviewGenerated.Invoke();
        }
        
        private void OnGamePause(bool arg0)
        {
            if (_selectedTurret != null)
                TurretPlacementFailed();
        }

        private void OnGameOver(bool arg0)
        {
            if (_selectedTurret != null)
                TurretPlacementFailed();
        }

        private MeshRenderer _collisionIndicator;
        private GameObject _collIndGO;
        private Transform _collIndT;

        /// <summary>
        /// create a simple collision indicator, wanted to do something using lines and rendering, but this works for prototype purposes 
        /// </summary>
        private void GenerateCollisionIndicator()
        {
            _collisionIndicator = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshRenderer>();
            _collisionIndicator.material = collisionMaterial;
            _collisionIndicator.material.color = acceptedPlacement;
            _collIndT = _collisionIndicator.transform;
            _collIndT.localScale = _halfExtends * 2;
            _collIndGO = _collisionIndicator.gameObject;
            _collIndGO.SetActive(false);
            Destroy(_collisionIndicator.GetComponent<Collider>());
        }

        private void ResetCollisionIndicator()
        {
            _collIndT.SetParent(null);
            _collIndGO.SetActive(false);
        }

        private void EnableCollisionIndicator()
        {
            if (_selectedTurret == null)
                return;
            _collIndT.SetParent(_selectedTurret.transform);
            _collIndT.localPosition = Vector3.zero;
            _collIndGO.SetActive(true);
        }
    }
}