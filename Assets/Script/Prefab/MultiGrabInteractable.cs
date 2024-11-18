using UnityEngine;
using Fusion;

namespace Prefab
{
    [RequireComponent(typeof(Rigidbody))]
    public class MultiGrabInteractable : NetworkBehaviour
    {
        [SerializeField] private Color _color;

        [Networked] public float _colorRate { get; private set; }

        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private Vector3 _positionPrevious = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;
        private int _handCount = 0;

        public override void Spawned()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority == true)
            {
                if (_handCount != 0)
                {
                    _velocity = (this.transform.position - _positionPrevious) / Runner.DeltaTime;
                    _positionPrevious = this.transform.position;
                }
            }

            if (_colorRate < 1) _colorRate += 0.01f;
        }

        public override void Render()
        {
            _meshRenderer.material.SetColor("_BaseColor", Color.Lerp(Color.white, _color, _colorRate));
        }

        public void IsGrab(Transform hand)
        {
            _colorRate = 0;
            _handCount++;

            this.transform.SetParent(hand);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
            _rigidbody.isKinematic = true;
        }

        public void IsRelease()
        {
            if (--_handCount == 0)
            {
                this.transform.SetParent(null);
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(_velocity * 50f);
            }
        }
    }
}
