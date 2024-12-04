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
        //������MultiPlayer�͕`����~����B�i���Z���͍̂s����j
        [Header("Active Component")]
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private MeshRenderer _headRenderer;
        [SerializeField] private MeshRenderer _leftHandRenderer;
        [SerializeField] private MeshRenderer _rightHandRenderer;

        /// <summary>
        /// �X�|�[�����ɌĂяo�����B
        /// </summary>
        public override void Spawned()
        {
            //�z�X�g�݂̂����������
            if (Object.HasInputAuthority == true)
            {
                _collider.enabled = false;
                _headRenderer.enabled = false;
                _leftHandRenderer.enabled = false;
                _rightHandRenderer.enabled = false;
            }
        }

        /// <summary>
        /// FixedUpdate��Fusion��
        /// </summary>
        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority == true)
            {
                //���͂��擾����
                if (GetInput(out NetworkInputData data))
                {
                    //�ʒu�̎擾
                    this.transform.position = data.position;
                    this.transform.rotation = data.rotation;

                    _head.localPosition = data.head.localPosition;
                    _head.localRotation = data.head.localRotation;
                    _leftHand.localPosition = data.leftHand.localPosition;
                    _leftHand.localRotation = data.leftHand.localRotation;
                    _rightHand.localPosition = data.rightHand.localPosition;
                    _rightHand.localRotation = data.rightHand.localRotation;
                }
            }
        }

        /// <summary>
        /// Update��Fusion��
        /// </summary>
        public override void Render()
        {
            //�R���C�_�[�̈ʒu�𒲐�����
            Vector3 center = new Vector3(
                _head.localPosition.x,
                _collider.center.y,
                _head.localPosition.z);
            _collider.center = center;
        }
    }
}
