using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ld46.Data;
using ld46.Components;

namespace ld46.Components {

    [ 
        RequireComponent( typeof( Animator ) )
    ]
    public class Actor : MonoBehaviour
    {
        public enum Type {
            PLAYER,
            ENEMY,
        }

        public enum Stance {
            IDLE,
            ATTACKING,
            BLOCKING,
            DODGING_LEFT,
            DODGING_RIGHT,
            HIT,
            DEAD,
        }

        [ SerializeField ] protected FloatValue m_health;
        [ SerializeField ] protected FloatValue m_maxHealth;
        [ SerializeField ] protected Actor m_oponent;
        [ SerializeField ] protected bool m_canAutoblock;
        [ SerializeField ] protected bool m_canParry;

        private Animator m_animator;
        
        public bool CanBeParried { get; set; } = false;

        void Start()
        {
            m_animator = GetComponent< Animator >();
            m_health.Reset( m_maxHealth.Value, false );
        }

        private void OnEnable() 
        {
            if ( m_health != null ) m_health.OnValueChanged += this.OnHealthChanged;
        }

        private void OnDisable() 
        {
            if ( m_health != null ) m_health.OnValueChanged -= this.OnHealthChanged;
        }

        private void OnHealthChanged( FloatValue value )
        {
            if ( m_health.Value <= 0 ) {
                m_animator.SetTrigger( "isDead" );     
                this.enabled = false;
            } 
            else if ( this.GetStance() != Stance.HIT ) {
                m_animator.SetTrigger( "isHit" );
            }
        }

        public bool TriggerAutoBlock()
        {
            if ( m_canAutoblock ) {
                m_animator.SetTrigger( "isBlocking" );
                return true;
            }
            return false;
        }

        public void OnHit( float damage )
        {
            Stance stance = GetStance();
            if ( stance == Stance.IDLE && TriggerAutoBlock() ) {
                return;
            }

            CanBeParried = false;
            m_health.Value -= damage;
        }

        public void Attack( AttackInfo attack )
        {
            Stance oponentStance = m_oponent.GetStance();

            if ( oponentStance == Stance.DEAD ) {
                Debug.Log("Target is dead");
                return;
            }

            if ( oponentStance == Stance.BLOCKING ) {
                m_animator.SetTrigger( "isHit" );
                Debug.Log("Blocking. No damage");
                return;
            }

            if ( oponentStance == Stance.ATTACKING && m_canParry ) {
                if ( m_oponent.CanBeParried ) {
                    m_oponent.OnHit( attack.Damage );
                    return;
                }
                else if ( m_oponent.TriggerAutoBlock() ) {
                    return;
                }
            }

            if ( oponentStance == Stance.ATTACKING && ( !m_canParry || !m_oponent.CanBeParried ) ) {
                Debug.Log("Attacking. No damage");
                return;
            }
            
            if ( attack.Direction == AttackDirection.LEFT && oponentStance == Stance.DODGING_RIGHT ) {
                this.CanBeParried = true;
                Debug.Log("No damage");
                return;
            }

            if ( attack.Direction == AttackDirection.RIGHT && oponentStance == Stance.DODGING_LEFT ) {
                this.CanBeParried = true;
                Debug.Log("No damage");
                return;
            }

            if ( attack.Direction == AttackDirection.TOP && oponentStance != Stance.IDLE ) {
                this.CanBeParried = true;
                Debug.Log("No damage");
                return;
            }

            m_oponent.OnHit( attack.Damage );
        }

        public Stance GetStance()
        {
            AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
            if ( info.IsTag( "Attacking" ) ) {
                return Stance.ATTACKING;
            }
            else if ( info.IsTag( "Dodging Left" ) ) {
                return Stance.DODGING_LEFT;
            }
            else if ( info.IsTag( "Dodging Right" ) ) {
                return Stance.DODGING_RIGHT;
            }
            else if ( info.IsTag( "Blocking" ) ) {
                return Stance.BLOCKING;
            }
            else if ( info.IsTag( "Dead" ) ) {
                return Stance.DEAD;
            }
            else if ( info.IsTag( "Hit" ) ) {
                return Stance.HIT;
            }
            return Stance.IDLE;
        }

        public void OpenForAttack( int open )
        {
            this.CanBeParried = ( open == 1 );
        }

    }

}

