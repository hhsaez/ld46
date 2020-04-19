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

        public void Attack( float damage )
        {            
            m_oponentHealth.Value -= damage;
        }

    }

}

