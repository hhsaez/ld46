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
        [ SerializeField ] protected FloatValue m_health;
        [ SerializeField ] protected FloatValue m_maxHealth;
        [ SerializeField ] protected FloatValue m_oponentHealth;

        private Animator m_animator;

        void Start()
        {
            m_animator = GetComponent< Animator >();
            m_health.Value = m_maxHealth.Value;
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
        }

        public void Attack( float damage )
        {
            m_oponentHealth.Value -= damage;
        }

    }

}

