using TowerDefense.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefense.Turrets
{
    public class TurretPlacer : MonoBehaviour
    {
        public LayerMask overlapLayers;
        
        private Camera _placerCamera;

        private Vector3? HitPoint = null;

        private readonly Collider[] _obstacleOverlap = new Collider[1];

        public Turret selectedTurret;

        private void Awake()
        {
            _placerCamera = Camera.main;
        }

        private void Start() {
            InputHandler.TurretPlacer.PlaceTurret.performed += OnPlaceTurret;
        }

        private void OnPlaceTurret(InputAction.CallbackContext context)
        {
            var placerPosition = InputHandler.TurretPlacer.PlacerPosition.ReadValue<Vector2>();

            var ray = _placerCamera.ScreenPointToRay(placerPosition);

            if (!Physics.Raycast(ray, out var hit, 100)) 
                return;
            
            // Draw a ray if hit with something
            Debug.DrawRay(ray.origin, ray.direction * hit.distance);

            if (hit.collider.tag.Equals("Battlefield"))
            {
                HitPoint = hit.point;
                        
                // We are only interested on knowing if there is at least 1 object blocking our placement of turrets :)
                var numOfObstacles = Physics.OverlapBoxNonAlloc(HitPoint.Value, Vector3.one, _obstacleOverlap, Quaternion.identity, overlapLayers);

                if (numOfObstacles > 0) {
                    return;
                }
                        
                // create turret at location
                Turret.Create(selectedTurret, hit.point);
            }
        }



        private void OnDrawGizmos()
        {
            if (HitPoint.HasValue)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(HitPoint.Value, Vector3.one * 2);
            }
        }
    }
}