using UnityEngine;
using Fusion;

namespace Prefab.Player
{
    public class MultiDirectInteractor : NetworkBehaviour
    {
        private MultiGrabInteractable _grabableObject;

        private void OnTriggerEnter(Collider other)
        {
            if (Object.HasStateAuthority)
            {
                if (TryGetComponent<MultiGrabInteractable>(out var grabable))
                {
                    _grabableObject = grabable;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Object.HasStateAuthority)
            {
                _grabableObject = null;
            }
        }
    }
}
