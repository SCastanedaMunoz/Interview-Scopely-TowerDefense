using UnityEngine;

namespace TowerDefense.Input
{
    /// <summary>
    /// simple Input initialization handler
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        /// <summary>
        /// direct access to master input
        /// </summary>
        public static MasterInput Input => _masterControls;

        /// <summary>
        /// access to camera actions
        /// </summary>
        public static MasterInput.CameraActions Camera => _masterControls.Camera;

        /// <summary>
        /// access to turret placer actions
        /// </summary>
        public static MasterInput.TurretPlacerActions TurretPlacer => _masterControls.TurretPlacer;

        /// <summary>
        /// references to our camera controls schema
        /// </summary>
        private static MasterInput _masterControls;

        /// <summary>
        /// automatically initialize :D
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            new GameObject("Input Handler").AddComponent<InputHandler>();
            _masterControls = new MasterInput();
            _masterControls.asset.Enable();
        }

        private void OnDestroy()
        {
            // disable camera controls input asset
            _masterControls.asset.Disable();
        }
    }
}