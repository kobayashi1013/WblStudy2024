using UnityEngine;
using Fusion;

namespace Scene.InGame
{
    public class RunnerBoot : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _runnerPrefab; //Runner�̃v���n�u

        private async void Start()
        {
            var runner = Instantiate(_runnerPrefab); //Runner���N���B
            runner.ProvideInput = true; //InputAuthority��^����悤�ɐݒ肷��B

            var startGameArgs = new StartGameArgs() //�Z�b�V�����̐ݒ������B
            {
                GameMode = GameMode.AutoHostOrClient, //�ŏ��ɓ���v���C���[�Ȃ�z�X�g�ƂȂ�B
                Scene = SceneRef.FromIndex(0),
                SceneManager = runner.GetComponent<NetworkSceneManagerDefault>(), //�V�[���J�ڂ�����R���|�[�l���g
                PlayerCount = 2, //�v���C���[�̍ő�l��
            };

            await runner.StartGame(startGameArgs); //�Z�b�V�����J�n

            Destroy(this.gameObject); //�s�v�ɂȂ����̂ō폜
        }
    }
}
