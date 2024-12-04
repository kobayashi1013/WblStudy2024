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
        //自分のMultiPlayerは描画を停止する。（演算自体は行われる）
        [Header("Active Component")]
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private MeshRenderer _headRenderer;
        [SerializeField] private MeshRenderer _leftHandRenderer;
        [SerializeField] private MeshRenderer _rightHandRenderer;

        /// <summary>
        /// スポーン時に呼び出される。
        /// </summary>
        public override void Spawned()
        {
            //ホストのみが処理される
            if (Object.HasInputAuthority == true)
            {
                _collider.enabled = false;
                _headRenderer.enabled = false;
                _leftHandRenderer.enabled = false;
                _rightHandRenderer.enabled = false;
            }
        }

        /// <summary>
        /// FixedUpdateのFusion版
        /// </summary>
        public override void FixedUpdateNetwork()
        {
            if (Object.HasStateAuthority == true)
            {
                //入力を取得する
                if (GetInput(out NetworkInputData data))
                {
                    //位置の取得
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
        /// UpdateのFusion版
        /// </summary>
        public override void Render()
        {
            //コライダーの位置を調整する
            Vector3 center = new Vector3(
                _head.localPosition.x,
                _collider.center.y,
                _head.localPosition.z);
            _collider.center = center;
        }
    }
}
