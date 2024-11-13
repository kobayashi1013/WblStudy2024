using UnityEngine;
using Fusion;

namespace Scene.InGame
{
    public class RunnerBoot : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _runnerPrefab; //Runnerのプレハブ

        private async void Start()
        {
            var runner = Instantiate(_runnerPrefab); //Runnerを起動。
            runner.ProvideInput = true; //InputAuthorityを与えるように設定する。

            var startGameArgs = new StartGameArgs() //セッションの設定をする。
            {
                GameMode = GameMode.AutoHostOrClient, //最初に入るプレイヤーならホストとなる。
                Scene = SceneRef.FromIndex(0),
                SceneManager = runner.GetComponent<NetworkSceneManagerDefault>(), //シーン遷移をするコンポーネント
                PlayerCount = 2, //プレイヤーの最大人数
            };

            await runner.StartGame(startGameArgs); //セッション開始

            Destroy(this.gameObject); //不要になったので削除
        }
    }
}
