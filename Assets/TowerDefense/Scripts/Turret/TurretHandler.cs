using TowerDefense.Currency;
using TowerDefense.Input;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Turrets
{
    public class TurretHandler : MonoBehaviour
    {
        public static TurretHandler Instance { get; private set; }

        public LayerMask raycastPreviewLayers = 1 << 9;

        public LayerMask overlapLayers;

        public Material collisionMaterial;

        private Camera _placerCamera;

        private readonly Collider[] _obstacleOverlap = new Collider[1];

        private Turret _selectedTurret;

        private float _selectedPrice;

        public Color acceptedPlacement;
        public Color deniedPlacement;

        private readonly Vector3 _halfExtends = new Vector3(1.75f, 2, 1.75f);

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

        private void Update()
        {
            if (_selectedTurret != null && InputHandler.TurretPlacer.PreviewTurret.ReadValue<float>() >= 1)
            {
                var placerPosition = InputHandler.TurretPlacer.PlacerPosition.ReadValue<Vector2>();
                var ray = _placerCamera.ScreenPointToRay(placerPosition);

                if (!Physics.Raycast(ray, out var hit, 100, raycastPreviewLayers))
                    return;

                _selectedTurret.transform.position = hit.point;

                if (!CurrencyHandler.Instance.CanGoldPurchase(_selectedPrice)) {
                    _collisionIndicator.material.color = deniedPlacement;
                    _canPlace = false;
                    return;
                }

                var numOfObstacles = Physics.OverlapBoxNonAlloc(hit.point, _halfExtends, _obstacleOverlap,
                    Quaternion.identity, overlapLayers);

                if (numOfObstacles > 0)
                {
                    _collisionIndicator.material.color = deniedPlacement;
                    _canPlace = false;
                    return;
                }

                _collisionIndicator.material.color = acceptedPlacement;
                _canPlace = true;
            }

            else if (_canPlace && _selectedTurret != null &&
                     InputHandler.TurretPlacer.PreviewTurret.ReadValue<float>() <= 0)
            {
                if (!CurrencyHandler.Instance.GoldPurchase(_selectedPrice))
                {
                    TurretPlacementFailed();
                    return;
                }

                TurretPlacementSucceed();
            }

            else if (!_canPlace && _selectedTurret != null &&
                     InputHandler.TurretPlacer.PreviewTurret.ReadValue<float>() <= 0)
            {
                TurretPlacementFailed();
            }
        }

        private void TurretPlacementSucceed()
        {
            onTurretPreviewPlaced.Invoke();
            _canPlace = false;
            ResetCollisionIndicator();
            _selectedTurret.Activate();
            _selectedTurret = null;
        }

        private void TurretPlacementFailed()
        {
            onTurretPreviewCancelled.Invoke();
            ResetCollisionIndicator();
            Destroy(_selectedTurret.gameObject);
        }

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

        private MeshRenderer _collisionIndicator;
        private GameObject _collIndGO;
        private Transform _collIndT;

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