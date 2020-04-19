using UnityEngine;

namespace ld46.Data {

    public enum AttackDirection {
        FRONT,
        TOP,
        RIGHT,
        LEFT,
    }

    [
        CreateAssetMenu(
            fileName = "Attack Info", 
            menuName = "ld46/Data/Attack Info"
        )
    ]
    public class AttackInfo : ScriptableObject {

        [ SerializeField ] protected float m_damage;
        [ SerializeField ] protected AttackDirection m_direction;

        public float Damage 
        {
            get 
            {
                return m_damage;
            }
        }

        public AttackDirection Direction 
        {
            get
            {
                return m_direction;
            }
        }

    }

}

