using UnityEngine;

namespace ld46.Data {

    [
        CreateAssetMenu(
            fileName = "Int Value", 
            menuName = "ld46/Data/Int Value"
        )
    ]
    public class IntValue : ScriptableObject {

        public delegate void Delegate( IntValue value );
        public event Delegate OnValueChanged;

        protected void RaiseValueChanged()
        {
            this.OnValueChanged?.Invoke( this );
        }

        [SerializeField] protected int m_value;
        public int Value 
        {
            get 
            {
                return m_value;
            }
            set {
                m_value = value;
                RaiseValueChanged();
            }
        }

        public void Reset( int value, bool raise = true ) 
        {
            m_value = value;
            if ( raise ) {
                this.RaiseValueChanged();
            }
        }

    }

}

