using UnityEngine;
using Fusion;

namespace Network
{
    //NetworkInput�ɂ� (INetworkInput) �̌p�����K�v
    public struct NetworkInputData : INetworkInput
    {
        public Vector3 position; //�v���C���[�̈ʒu
        public Quaternion rotation; //�v���C���[�̉�]
        public Head head; //HMD
        public Hand leftHand; //���R���g���[���[
        public Hand rightHand; //�E�R���g���[���[

        public struct Head : INetworkInput
        {
            public Vector3 localPosition; //HMD�̈ʒu
            public Quaternion localRotation; //HMD�̉�]
        }

        public struct Hand : INetworkInput
        {
            public Vector3 localPosition; //Controller�̈ʒu
            public Quaternion localRotation; //Controller�̉�]
            public NetworkButtons buttons; //Controller�̃{�^������
        }

        public enum Buttons
        {
            Select, //������I������i�͂ށj�{�^��
        }
    }
}
