using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.Boss.MiniBoss.State
{
    public class MeleeAttackState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public MeleeAttackState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("MelAttackState!");
            _MiniBossEnemy._isAttacking = true;
            _MiniBossEnemy.MelAttackPatternStart();
            _MiniBossEnemy.GroggyEnter();
        }

        public void Update()
        {
            if (!_MiniBossEnemy._isAttacking)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.TurmState);
            }
        }

        public void Exit()
        {
            _MiniBossEnemy.DeActivateAttackArea();
        }
    }
}