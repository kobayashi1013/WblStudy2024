using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

namespace Network
{
    public class RunnerCallbacks : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkObject _multiPlayerPrefab; //MultiPlayer�v���n�u

        /// <summary>
        /// �v���C���[���Z�b�V�����ɎQ���������ɌĂяo�����B
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="player"></param>
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer == true) //�z�X�g�̂ݏ������s���B
            {
                var playerObj = runner.Spawn(_multiPlayerPrefab, Vector3.zero, Quaternion.identity, player); //�X�|�[���̓z�X�g�����s���Ȃ��B
                runner.SetPlayerObject(player, playerObj); //Runner�ɃX�|�[�������I�u�W�F�N�g��o�^����B
            }
        }

        /// <summary>
        /// �v���C���[���Z�b�V��������ޏo�������ɌĂяo�����B
        /// </summary>
        /// <param name="runner"></param>
        /// <param name="player"></param>
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer == true)
            {
                if (runner.TryGetPlayerObject(player, out var playerObj))
                {
                    runner.Despawn(playerObj);
                }
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    }
}
