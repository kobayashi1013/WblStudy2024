using UnityEngine;
using Fusion;

namespace Prefab
{
    [RequireComponent(typeof(Rigidbody))]
    public class MultiGrabInteractable : NetworkBehaviour
    {
        private static readonly float FORCE_CONST = 50f;
        private Rigidbody _rigidbody;
        private Vector3 _positionPrevious = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;
        private int _handCount = 0;
        public bool isGrab { get; private set; } = false;

        public override void Spawned()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority == true)
            {
                _velocity = (this.transform.position - _positionPrevious) / Runner.DeltaTime; //物理挙動の処理はFixedUpdateNetworkに書く
                _positionPrevious = this.transform.position;
            }
        }

        //掴む
        public void IsGrab(Transform hand)
        {
            if (Object.HasStateAuthority == true)
            {
                _handCount++;

                isGrab = true;
                this.transform.SetParent(hand); //handオブジェクトを親オブジェクトとする
                this.transform.localPosition = Vector3.zero;
                this.transform.localRotation = Quaternion.identity;
                _rigidbody.isKinematic = true; //物理挙動をオフにする
            }
        }

        //離す
        public void IsRelease()
        {
            if (Object.HasStateAuthority == true)
            {
                if (--_handCount == 0)
                {
                    isGrab = false;
                    this.transform.SetParent(null);
                    _rigidbody.isKinematic = false;
                    _rigidbody.AddForce(_velocity * FORCE_CONST);
                }
            }
        }
    }
}
