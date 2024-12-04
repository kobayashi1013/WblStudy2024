using UnityEngine;
using Fusion;

namespace Network
{
    //NetworkInputには (INetworkInput) の継承が必要
    public struct NetworkInputData : INetworkInput
    {
        public Vector3 position; //プレイヤーの位置
        public Quaternion rotation; //プレイヤーの回転
        public Head head; //HMD
        public Hand leftHand; //左コントローラー
        public Hand rightHand; //右コントローラー

        public struct Head : INetworkInput
        {
            public Vector3 localPosition; //HMDの位置
            public Quaternion localRotation; //HMDの回転
        }

        public struct Hand : INetworkInput
        {
            public Vector3 localPosition; //Controllerの位置
            public Quaternion localRotation; //Controllerの回転
            public NetworkButtons buttons; //Controllerのボタン入力
        }

        public enum Buttons
        {
            Select, //何かを選択する（掴む）ボタン
        }
    }
}
