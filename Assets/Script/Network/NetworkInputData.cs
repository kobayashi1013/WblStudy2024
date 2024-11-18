using UnityEngine;
using Fusion;

namespace Network
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector3 position;
        public Quaternion rotation;
        public Head head;
        public Hand leftHand;
        public Hand rightHand;

        public struct Head : INetworkStruct
        {
            public Vector3 localPosition;
            public Quaternion localRotation;
        }

        public struct Hand : INetworkStruct
        {
            public Vector3 localPosition;
            public Quaternion localRotation;
            public NetworkButtons buttons;
        }

        public enum Buttons
        {
            Select,
        }
    }
}
