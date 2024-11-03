using UnityEngine;

namespace Prefab
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab; //プレイヤーのプレハブ

        void Start()
        {
            Vector3 position = this.transform.position; //スポーンする位置
            Quaternion rotation = Quaternion.identity; //スポーンするプレイヤーの最初の向き

            Instantiate(_playerPrefab, position, rotation); //プレイヤーをスポーン

            Destroy(this.gameObject); //このスポナーは不要なので削除
        }
    }
}
