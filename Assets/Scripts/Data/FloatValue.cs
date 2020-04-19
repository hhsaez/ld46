using UnityEngine;

namespace ld46.Data {

    [
        CreateAssetMenu(
            fileName = "Float Value", 
            menuName = "ld46/Data/Float Value"
        )
    ]
    public class FloatValue : ScriptableObject {

        public delegate void Delegate( FloatValue value );
        public event Delegate OnValueChanged;

        protected void RaiseValueChanged()
        {
            this.OnValueChanged?.Invoke( this );
        }

        [SerializeField] protected float m_value;
        public float Value 
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

        public void Reset( float value, bool raise = true ) 
        {
            m_value = value;
            if ( raise ) {
                this.RaiseValueChanged();
            }
        }

    }

}

