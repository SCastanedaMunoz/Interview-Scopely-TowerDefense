using System;
using TowerDefense.Input;
using UnityEngine;

namespace TowerDefense.Turrets
{
    public class TurretPlacer : MonoBehaviour
    {
        public LayerMask overlapLayers;
        
        private Camera _placerCamera;

        private Vector3? HitPoint = null;

        private Collider[] _overlap = new Collider[1];

        private void Awake()
        {
            _placerCamera = Camera.main;
        }

        private void Start()
        {
            InputHandler.TurretPlacer.PlaceTurret.performed += cxt =>
            {
                var placerPosition = InputHandler.TurretPlacer.PlacerPosition.ReadValue<Vector2>();

                var ray = _placerCamera.ScreenPointToRay(placerPosition);

                if (Physics.Raycast(ray, out var hit, 100))
                {
                    // Draw a ray if hit with something
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance);

                    if (hit.collider.tag.Equals("Battlefield"))
                    {
                        HitPoint = hit.point;
                        
                        var i = Physics.OverlapBoxNonAlloc(HitPoint.Value, Vector3.one, _overlap, Quaternion.identity, overlapLayers);

                        if (i > 0) {
                            Debug.LogError("Not an empty space, can't instantiate");
                            _overlap[0] = null;
                            return;
                        }
                        
                        Debug.LogError("Ready to instantiate!");
                        
                        // instantiate turret
                    }
                }
            };
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