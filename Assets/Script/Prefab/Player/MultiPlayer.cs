using UnityEngine;
using Fusion;
using Network;

namespace Prefab.Player
{
    public class MultiPlayer : NetworkBehaviour
    {
        [Header("Body Objects")]
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [Header("Active Component")]
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private MeshRenderer _headRenderer;
        [SerializeField] private MeshRenderer _leftHandRenderer;
        [SerializeField] private MeshRenderer _rightHandRenderer;

        public override void Spawned()
        {
            if (Object.HasInputAuthority == true)
            {
                _collider.enabled = false;
                _headRenderer.enabled = false;
                _leftHandRenderer.enabled = false;
                _rightHandRenderer.enabled = false;
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority)
            {
                if (GetInput(out NetworkInputData data))
                {
                    this.transform.position = data.position;
                    this.transform.rotation = data.rotation;

                    _head.localPosition = data.head.localPosition;
                    _head.localRotation = data.head.localRotation;
                    _leftHand.localPosition = data.leftHand.localPosition;
                    _leftHand.localRotation = data.leftHand.localRotation;
                    _rightHand.localPosition = data.rightHand.localPosition;
                    _rightHand.localRotation = data.rightHand.localRotation;

                    Vector3 colliderPosition = new Vector3(
                        data.head.localPosition.x,
                        _collider.center.y,
                        data.head.localPosition.z);
                    _collider.center = colliderPosition;
                }
            }
        }
    }
}
