using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Model.Entities.Components
{
    [RequireComponent(typeof(MovementComponent))]
    public class FollowPlayerComponent: EntityComponent
    {
        [Inject] private readonly Player.Player _player;

        private MovementComponent _movementComponent;

        private void OnEnable()
        {
            _movementComponent = GetComponent<MovementComponent>();
        }

        private void FixedUpdate()
        {
            _movementComponent.SetVelocity(GetDirection());
        }

        private Vector2 GetDirection()
        {
            var direction = Vector2.zero;
            
            var path = new NavMeshPath();
            var startPos = (Vector2)transform.position;
            var endPos = (Vector2)_player.transform.position;

            if (!NavMesh.SamplePosition(endPos, out NavMeshHit hit, 2f, NavMesh.AllAreas)) 
                return direction;
            if (!NavMesh.CalculatePath(startPos, hit.position, NavMesh.AllAreas, path)) 
                return direction;
            
            var pathPoints = path.corners;
            if (pathPoints.Length > 1)
            {
                direction = ((Vector2)pathPoints[1] - startPos).normalized;
            }

            return direction;
        }
    }
}