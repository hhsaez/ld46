using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ld46.Data;

namespace ld46.Components {

    [ 
        RequireComponent( typeof( Animator ) )
    ]
    public class Actor : BaseComponent
    {
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

        void Start()
        {
            m_animator = GetComponent< Animator >();
            m_health.Reset( m_maxHealth.Value, false );
        }

        private void OnEnable() 
        {
            if ( m_health != null ) {
                m_health.OnValueChanged += this.OnHealthChanged;
            }
        }

        private void OnDisable() 
        {
            if ( m_health != null ) {
                m_health.OnValueChanged -= this.OnHealthChanged;
            }
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

        public void OnHit( float damage )
        {
            Stance stance = GetStance();
            if ( m_canAutoblock && stance == Stance.IDLE ) {
                m_animator.SetTrigger( "isBlocking" );
                return;
            }

            m_health.Value -= damage;
        }

        public void Attack( AttackInfo attack )
        {
            Debug.Log("Direction " + attack.Direction + " damage " + attack.Damage);

            Stance oponentStance = m_oponent.GetStance();
            Debug.Log( "Oponent: " + oponentStance );

            if ( oponentStance == Stance.DEAD ) {
                Debug.Log("Target is dead");
                return;
            }

            if ( oponentStance == Stance.BLOCKING ) {
                m_animator.SetTrigger( "isHit" );
                Debug.Log("Blocking. No damage");
                return;
            }
            
            if ( attack.Direction == AttackDirection.LEFT && oponentStance == Stance.DODGING_RIGHT ) {
                Debug.Log("No damage");
                return;
            }

            if ( attack.Direction == AttackDirection.RIGHT && oponentStance == Stance.DODGING_LEFT ) {
                Debug.Log("No damage");
                return;
            }

            if ( attack.Direction == AttackDirection.TOP && oponentStance != Stance.IDLE ) {
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

    }

}

