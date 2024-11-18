using UnityEngine;
using Fusion;
using Network;

namespace Prefab.Player
{
    public class MultiDirectInteractor : NetworkBehaviour
    {
        private enum HandType
        {
            LeftHand,
            RightHand,
        }

        [SerializeField] private HandType _handType;
        private MultiGrabInteractable _contactObject;
        private MultiGrabInteractable _grabableObject;
        private NetworkButtons _handPrevious;

        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority)
            {
                if (GetInput(out NetworkInputData data))
                {
                    
                    if (_handType == HandType.LeftHand)
                    {
                        _handPrevious = GrabAction(data.leftHand, _handPrevious);
                    }
                    else if (_handType == HandType.RightHand)
                    {
                        _handPrevious = GrabAction(data.rightHand, _handPrevious);
                    }
                }
            }
        }

        private NetworkButtons GrabAction(NetworkInputData.Hand hand, NetworkButtons prev)
        {
            NetworkButtons pressed = hand.buttons.GetPressed(prev);
            NetworkButtons released = hand.buttons.GetReleased(prev);

            if (pressed.IsSet(NetworkInputData.Buttons.Select) && _contactObject != null)
            {
                _grabableObject = _contactObject;
                _grabableObject.IsGrab(this.transform);
            }
            else if (released.IsSet(NetworkInputData.Buttons.Select) && _grabableObject != null)
            {
                _grabableObject.IsRelease();
                _grabableObject = null;
            }

            return hand.buttons;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Object.HasStateAuthority)
            {
                if (other.TryGetComponent<MultiGrabInteractable>(out var grabable))
                {
                    _contactObject = grabable;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Object.HasStateAuthority)
            {
                if (other.TryGetComponent<MultiGrabInteractable>(out var _))
                {
                    _contactObject = null;
                }
            }
        }
    }
}
