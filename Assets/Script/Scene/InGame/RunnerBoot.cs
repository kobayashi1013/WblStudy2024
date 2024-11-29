using UnityEngine;
using Fusion;

namespace Scene.InGame
{
    public class RunnerBoot : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _runnerPrefab; //Runner�v���n�u

        private async void Start()
        {
            var runner = Instantiate(_runnerPrefab); //Runner���N��
            runner.ProvideInput = true; //InputAuthority���g����悤�ɂ���B

            var startGameArgs = new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient, //�ŏ��ɓ���v���C���[���z�X�g�ƂȂ�
                Scene = SceneRef.FromIndex(0), //�Z�b�V�����J�n���̃V�[���C���f�b�N�X
                SceneManager = runner.GetComponent<NetworkSceneManagerDefault>(), //�V�[����J�ڂ���R���|�[�l���g
                PlayerCount = 4, //�v���C���[�̍ő�l��
            };

            var result = await runner.StartGame(startGameArgs); //�Z�b�V�����J�n
            if (result.Ok == true)
            {
                if (runner.IsServer == true) Debug.Log("Session Role : Host");
                else Debug.Log("Session Role : Client");
            }

            Destroy(this.gameObject); //�s�v�ɂȂ����̂ō폜
        }
    }
}
