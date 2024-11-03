using UnityEngine;

namespace Prefab.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _rotationSensitive;

        private CharacterController _characterController; //キャラクターコントローラー

        private void Start()
        {
            //もしキャラクターコントローラーコンポーネントがこのオブジェクトにアタッチされていたら取得する。
            if (TryGetComponent<CharacterController>(out var controller)) _characterController = controller;
        }

        private void Update()
        {
            PlayerMove(); //プレイヤーの移動
            PlayerRotate(); //プレイヤーの回転
        }

        /// <summary>
        /// プレイヤーの移動
        /// </summary>
        private void PlayerMove()
        {
            //入力を取得
            float horizontal = Input.GetAxis("Horizontal"); //左右
            float vertical = Input.GetAxis("Vertical"); //上下

            //移動方向の計算
            Vector3 inputDirection = new Vector3(horizontal, 0f, vertical); //入力した方向（上下右左）
            Vector3 localDirection = transform.TransformDirection(inputDirection); //ローカル座標を考慮した方向に調整
            Vector3 movement = localDirection * _walkSpeed;

            _characterController.Move(movement * Time.deltaTime); //座標移動
        }

        /// <summary>
        /// プレイヤーの回転
        /// </summary>
        private void PlayerRotate()
        {
            //入力を取得
            float mouseX = Input.GetAxis("Mouse X"); //マウスX軸の１フレーム移動量（座標ではない）
 
            //回転の計算
            Vector3 inputRotate = new Vector3(0f, mouseX, 0f); //軸の回転
            Vector3 rotate = inputRotate * _rotationSensitive;

            transform.Rotate(rotate); //回転
        }
    }
}
