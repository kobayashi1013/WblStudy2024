using UnityEngine;
using UnityEngine.InputSystem;

namespace Prefab.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _rotationSensitive;

        private CharacterController _characterController; //�L�����N�^�[�R���g���[���[
        private Vector2 _moveInput = Vector2.zero; //���͂̎擾
        private Vector2 _rotateInput = Vector2.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            _rotateInput = context.ReadValue<Vector2>();
        }

        private void Start()
        {
            //�����L�����N�^�[�R���g���[���[�R���|�[�l���g�����̃I�u�W�F�N�g�ɃA�^�b�`����Ă�����擾����B
            if (TryGetComponent<CharacterController>(out var controller)) _characterController = controller;
        }

        private void Update()
        {
            PlayerMove(); //�v���C���[�̈ړ�
            PlayerRotate(); //�v���C���[�̉�]
        }

        /// <summary>
        /// �v���C���[�̈ړ�
        /// </summary>
        private void PlayerMove()
        {
            //���͂��擾
            float horizontal = _moveInput.x; //���E
            float vertical = _moveInput.y; //�㉺

            //�ړ������̌v�Z
            Vector3 inputDirection = new Vector3(horizontal, 0f, vertical); //���͂��������i�㉺�E���j
            Vector3 localDirection = transform.TransformDirection(inputDirection); //���[�J�����W���l�����������ɒ���
            Vector3 movement = localDirection * _walkSpeed;

            _characterController.Move(movement * Time.deltaTime); //���W�ړ�
        }

        /// <summary>
        /// �v���C���[�̉�]
        /// </summary>
        private void PlayerRotate()
        {
            //���͂��擾
            float mouseX = _rotateInput.x; //�}�E�XX���̂P�t���[���ړ��ʁi���W�ł͂Ȃ��j
 
            //��]�̌v�Z
            Vector3 inputRotate = new Vector3(0f, mouseX, 0f); //���̉�]
            Vector3 rotate = inputRotate * _rotationSensitive;

            transform.Rotate(rotate); //��]
        }
    }
}
