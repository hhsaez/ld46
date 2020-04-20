using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ld46.Data;

namespace ld46.Components {

    public class EnemyController : MonoBehaviour
    {
        [ SerializeField ] protected FloatValue m_health;
        [ SerializeField ] protected FloatValue m_maxHealth;

        private Actor m_actor;
        private Animator m_animator;
        private List< string > m_attacks = new List< string >();
        private List< string > m_attackQueue = new List< string >();
        private float m_actionTimer = 0.0f;

        private void Start()
        {
            m_actor = GetComponent< Actor >();
            m_animator = GetComponent< Animator >();

            m_attacks = new List<string>() {
                "isAttackingLeft",
                "isAttackingRight",
                "isAttackingCenter",
            };
        }

        private void Update() 
        {
            if ( Time.timeScale == 0 ) {
                return;
            }

            m_actionTimer += Time.deltaTime;

            if ( m_actionTimer >= 3.0f ) {
                if ( m_attackQueue.Count == 0 ) {
                    m_attackQueue = new List< string >( m_attacks.Randomize() );
                }
                m_actor.CanBeParried = false;
                string nextAttack = m_attackQueue[ 0 ];
                m_attackQueue.RemoveAt( 0 );
                m_animator.SetTrigger( nextAttack );
                m_animator.speed = 2.0f - (m_health.Value / m_maxHealth.Value);
                m_actionTimer = 0.0f;
            }
        }

    }

}

