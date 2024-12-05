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
        private NetworkButtons _buttonPrevious;

        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority == true)
            {
                if (GetInput(out NetworkInputData data))
                {
                    if (_handType == HandType.LeftHand)
                        _buttonPrevious = GrabAction(data.leftHand, _buttonPrevious);
                    else if (_handType == HandType.RightHand)
                        _buttonPrevious = GrabAction(data.rightHand, _buttonPrevious);
                }
            }
        }

        //íÕÇÒÇ≈Ç¢ÇÈÇ©ÇîªíËÇ∑ÇÈ
        private NetworkButtons GrabAction(NetworkInputData.Hand hand, NetworkButtons previous)
        {
            NetworkButtons pressed = hand.buttons.GetPressed(previous);
            NetworkButtons released = hand.buttons.GetReleased(previous);

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

        //ColliderÇ…ï®ëÃÇ™ì¸ÇÈÇ∆åƒÇ—èoÇ≥ÇÍÇÈÅB
        private void OnTriggerEnter(Collider other)
        {
            if (Object.HasStateAuthority == true)
            {
                if (other.TryGetComponent<MultiGrabInteractable>(out var grabable))
                {
                    _contactObject = grabable;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Object.HasStateAuthority == true)
            {
                if (other.TryGetComponent<MultiGrabInteractable>(out var _))
                {
                    _contactObject = null;
                }
            }
        }
    }
}
