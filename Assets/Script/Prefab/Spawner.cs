using Unity.XR.CoreUtils;
using UnityEngine;

namespace Prefab
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private XROrigin _playerPrefab; //�v���C���[�̃v���n�u

        private void Start()
        {
            Vector3 position = this.transform.position; //�X�|�[������ʒu
            Quaternion rotation = Quaternion.identity; //�X�|�[������v���C���[�̍ŏ��̌���

            Instantiate(_playerPrefab, position, rotation); //�v���C���[���X�|�[��

            Destroy(this.gameObject); //���̃X�|�i�[�͕s�v�Ȃ̂ō폜
        }
    }
}
