using Moonthsoft.Core.Definitions.Direction;
using Moonthsoft.Core.Managers;
using UnityEngine;
using Zenject;

namespace Moonthsoft.PacMan
{
    public abstract class Character : MonoBehaviour
    {
        private const float SPEED = 5f;

        [SerializeField] protected Animator animator;

        protected bool isMoving = false;

        private LevelManager _levelManager;

        public LevelManager LevelManager { get { return _levelManager; } }

        public NodeGraph CurrentNode { get; set; }

        public Direction CurrentDir { get; protected set; }

        protected abstract void GetNextNode();

        protected abstract NodeGraph GetInitialNode();

        protected abstract float GetSpeedPercentage();

        [Inject] private void InjectLevelManager(LevelManager levelManager) { _levelManager = levelManager; }

        private void Awake()
        {
            ResetCharacter();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        public virtual void ResetCharacter()
        {
            CurrentNode = GetInitialNode();
            transform.position = CurrentNode.transform.position;

            animator.Rebind();
        }

        protected void Movement()
        {
            if (isMoving)
            {
                transform.position += SPEED * Time.deltaTime * GetSpeedPercentage() * (Vector3)GetVectorDirection(CurrentDir);

                CheckReachedNode();
            }
            else
            {
                GetNextNode();
            }
        }

        protected void SetMove(bool active)
        {
            isMoving = active;
            animator.SetBool("isMoving", active);
        }

        protected void SetDirection(Direction direction)
        {
            CurrentDir = direction;

            var movement = GetVectorDirection(direction);

            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
        }

        private void CheckReachedNode()
        {
            if (IsInNode(CurrentDir))
            {
                transform.position = CurrentNode.transform.position;

                SetMove(false);

                GetNextNode();
            }
        }

        private bool IsInNode(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return transform.position.y >= CurrentNode.transform.position.y;
                case Direction.Down: return transform.position.y <= CurrentNode.transform.position.y;
                case Direction.Left: return transform.position.x <= CurrentNode.transform.position.x;
                case Direction.Right: return transform.position.x >= CurrentNode.transform.position.x;
            }

            Debug.LogError("Incorrect direction");

            return false;
        }

        private Vector2 GetVectorDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return Vector2.up;
                case Direction.Down: return Vector2.down;
                case Direction.Left: return Vector2.left;
                case Direction.Right: return Vector2.right;
            }

            Debug.LogError("Incorrect direction");

            return Vector2.zero;
        }
    }
}