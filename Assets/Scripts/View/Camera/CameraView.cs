using Cinemachine;
using Model.Entities.Player;
using UnityEngine;
using Zenject;

namespace View.Camera
{
    public class CameraView: MonoBehaviour
    {
        [Inject] private readonly Player _player;

        private CinemachineVirtualCamera _virtualCamera;

        private void OnEnable()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            _virtualCamera.Follow = _player.transform;
        }
    }
}