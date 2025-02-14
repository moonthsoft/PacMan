using Moonthsoft.Core.Definitions.Sounds;
using Moonthsoft.Core.FSM;
using Moonthsoft.Core.Managers;
using Moonthsoft.Core.Utils.Direction;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Moonthsoft.PacMan
{
    public class Ghost : Character
    {
        [SerializeField] private GhostType _type;

        [SerializeField] private GhostState _initState;

        private NodeGraph _lastNode = null;
        private bool _speedUp = false;
        private bool _isFrightened = false;

        public GhostType Type { get { return _type; } }

        public GhostState InitState { get { return _initState; } }

        public bool IsFrightened { get { return _isFrightened; } }

        public BaseStateController<Ghost, GhostState> StateController { get; private set; }

        public void InitStateController(BaseStateController<Ghost, GhostState> stateController) { StateController = stateController; }

        public Animator Animator { get { return animator; } }

        public List<NodeGraph> Path { get; set; } = null;

        private void Start()
        {
            LevelManager.ActivePowerUpEvent += ActivePowerUp;
            LevelManager.FinishingPowerUpEvent += FinishingPowerUp;
            LevelManager.ResetPowerUpEvent += ResetPowerUp;
            LevelManager.DeactivePowerUpEvent += DeactivePowerUp;
            LevelManager.ChangeChaseModeEvent += ChangeChaseMode;
        }

        public void TurnAround()
        {
            (_lastNode, CurrentNode) = (CurrentNode, _lastNode);
            SetDirection(DirectionUtility.ReverseDirection(CurrentDir));
        }

        public override void ResetCharacter()
        {
            base.ResetCharacter();

            _isFrightened = false;

            animator.SetFloat("ghost", (float)_type);
        }

        public void Die()
        {
            DeactivePowerUp();

            StateController.SetState(GhostState.Eated);

            LevelManager.EatGhost();
        }

        protected override NodeGraph GetInitialNode()
        {
            return LevelManager.Graph.GetGhostInitialNode(Type);
        }

        protected override void GetNextNode()
        {
            _lastNode = CurrentNode;

            StateController.UpdateState();

            SetDirection(_lastNode.GetDirNode(CurrentNode));

            SetMove(true);
        }

        protected override float GetSpeedPercentage()
        {
            if (StateController.CurrentState == GhostState.Eated)
            {
                return LevelManager.GetSpeed(TypeSpeedPercentage.GhostEated);
            }
            else if (LevelManager.Graph.IsInTunnel(CurrentNode, _lastNode))
            {
                return LevelManager.GetSpeed(TypeSpeedPercentage.GhostTunnel);
            }
            else if (StateController.CurrentState == GhostState.Frightened)
            {
                return LevelManager.GetSpeed(TypeSpeedPercentage.GhostFrightened);
            }
            else if (_speedUp)
            {
                return LevelManager.GetSpeed(TypeSpeedPercentage.BlinkySpeedUp);
            }
            else
            {
                return LevelManager.GetSpeed(TypeSpeedPercentage.Ghost);
            }
        }

        private void ActivePowerUp()
        {
            if (StateController.CurrentState != GhostState.Eated)
            {
                _isFrightened = true;
                animator.SetBool("frightened", true);
                animator.SetBool("frightenedFinishing", false);

                if (StateController.CurrentState != GhostState.Home)
                {
                    StateController.SetState(GhostState.Frightened);
                }
            }
        }

        private void FinishingPowerUp()
        {
            animator.SetBool("frightenedFinishing", true);
        }

        private void ResetPowerUp()
        {
            animator.SetBool("frightenedFinishing", false);
        }

        private void DeactivePowerUp()
        {
            _isFrightened = false;
            animator.SetBool("frightened", false);
            animator.SetBool("frightenedFinishing", false);

            if (StateController.CurrentState == GhostState.Frightened)
            {
                StateController.SetState(GhostState.Scatter);
            }
        }

        private void ChangeChaseMode()
        {
            if (StateController.CurrentState == GhostState.Scatter)
            {
                StateController.SetState(GhostState.Chase);
            }
            else if (StateController.CurrentState == GhostState.Chase)
            {
                StateController.SetState(GhostState.Scatter);
            }
        }
    }
}