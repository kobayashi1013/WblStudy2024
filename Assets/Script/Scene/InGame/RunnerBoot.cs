using UnityEngine;
using Fusion;

namespace Scene.InGame
{
    public class RunnerBoot : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _runnerPrefab; //Runnerプレハブ

        private async void Start()
        {
            var runner = Instantiate(_runnerPrefab); //Runnerを起動
            runner.ProvideInput = true; //InputAuthorityを使えるようにする。

            var startGameArgs = new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient, //最初に入るプレイヤーがホストとなる
                Scene = SceneRef.FromIndex(0), //セッション開始時のシーンインデックス
                SceneManager = runner.GetComponent<NetworkSceneManagerDefault>(), //シーンを遷移するコンポーネント
                PlayerCount = 4, //プレイヤーの最大人数
            };

            var result = await runner.StartGame(startGameArgs); //セッション開始
            if (result.Ok == true)
            {
                if (runner.IsServer == true) Debug.Log("Session Role : Host");
                else Debug.Log("Session Role : Client");
            }

            Destroy(this.gameObject); //不要になったので削除
        }
    }
}
