using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using Prefab;

namespace Network
{
    public class RunnerCallback : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkObject _multiPlayerPrefab;
        [SerializeField] private NetworkObject _ballPrefab;
        [SerializeField] private Vector3 _ballPosition;
        [Header("Input Reference")]
        [SerializeField] private InputActionReference _headPosition;
        [SerializeField] private InputActionReference _headRosition;
        [SerializeField] private InputActionReference _leftHandPosition;
        [SerializeField] private InputActionReference _leftHandRosition;
        [SerializeField] private InputActionReference _leftHandSelect;
        [SerializeField] private InputActionReference _rightHandPosition;
        [SerializeField] private InputActionReference _rightHandRosition;
        [SerializeField] private InputActionReference _rightHandSelect;

        private XROrigin _xrOrigin;

        private void OnEnable()
        {
            _headPosition.action.Enable();
            _headRosition.action.Enable();
            _leftHandPosition.action.Enable();
            _leftHandRosition.action.Enable();
            _leftHandSelect.action.Enable();
            _rightHandPosition.action.Enable();
            _rightHandRosition.action.Enable();
            _rightHandSelect.action.Enable();
        }

        private void OnDisable()
        {
            _headPosition.action.Disable();
            _headRosition.action.Disable();
            _leftHandPosition.action.Disable();
            _leftHandRosition.action.Disable();
            _leftHandSelect.action.Disable();
            _rightHandPosition.action.Disable();
            _rightHandRosition.action.Disable();
            _rightHandSelect.action.Disable();
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer == true)
            {
                var playerObj = runner.Spawn(_multiPlayerPrefab, Vector3.zero, Quaternion.identity, player);
                runner.SetPlayerObject(player, playerObj);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (_xrOrigin != null)
            {
                NetworkInputData data = new NetworkInputData();

                data.position = _xrOrigin.transform.position;
                data.rotation = _xrOrigin.transform.rotation;

                data.head.localPosition = _headPosition.action.ReadValue<Vector3>();
                data.head.localRotation = _headRosition.action.ReadValue<Quaternion>();
                data.leftHand.localPosition = _leftHandPosition.action.ReadValue<Vector3>();
                data.leftHand.localRotation = _leftHandRosition.action.ReadValue<Quaternion>();
                data.leftHand.buttons.Set(NetworkInputData.Buttons.Select, _leftHandSelect.action.IsPressed());
                data.rightHand.localPosition = _rightHandPosition.action.ReadValue<Vector3>();
                data.rightHand.localRotation = _rightHandRosition.action.ReadValue<Quaternion>();
                data.rightHand.buttons.Set(NetworkInputData.Buttons.Select, _rightHandSelect.action.IsPressed());

                input.Set(data);
            }
        }

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

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            _xrOrigin = FindFirstObjectByType<XROrigin>();

            if (runner.IsServer == true)
            {
                runner.Spawn(_ballPrefab, _ballPosition, Quaternion.identity, null);
            }
        }

        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    }
}
