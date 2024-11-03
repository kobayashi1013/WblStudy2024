using UnityEngine;

namespace Prefab.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _rotationSensitive;

        private CharacterController _characterController; //�L�����N�^�[�R���g���[���[

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
            float horizontal = Input.GetAxis("Horizontal"); //���E
            float vertical = Input.GetAxis("Vertical"); //�㉺

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
            float mouseX = Input.GetAxis("Mouse X"); //�}�E�XX���̂P�t���[���ړ��ʁi���W�ł͂Ȃ��j
 
            //��]�̌v�Z
            Vector3 inputRotate = new Vector3(0f, mouseX, 0f); //���̉�]
            Vector3 rotate = inputRotate * _rotationSensitive;

            transform.Rotate(rotate); //��]
        }
    }
}
