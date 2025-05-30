using System.Collections.Generic;
using UnityEngine;
using Moonthsoft.Core.FSM;
using Moonthsoft.Core.Utils.Direction;

namespace Moonthsoft.PacMan
{
    /// <summary>
    /// Controls the logic of the Ghosts. Mainly movement and animations. 
    /// Also the initialization and calls to the StateController that is in charge of its AI.
    /// </summary>
    public class Ghost : Character
    {
        private NodeGraph _lastNode = null;
        private bool _speedUp = false;
        private bool _isFrightened = false;

        [SerializeField] private GhostType _type;
        [SerializeField] private GhostState _initState;

        public GhostType Type { get { return _type; } }
        public GhostState InitState { get { return _initState; } }
        public bool IsFrightened { get { return _isFrightened; } }
        public Animator Animator { get { return animator; } }

        public List<NodeGraph> Path { get; set; } = null;
        public BaseStateController<Ghost, GhostState> StateController { get; private set; }
        public void InitStateController(BaseStateController<Ghost, GhostState> stateController) { StateController = stateController; }


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

            LevelManager.EatGhost(transform.position);
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