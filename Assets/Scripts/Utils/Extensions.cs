using System;
using System.Linq;
using System.Collections.Generic;

namespace ld46 {

    public static class Extensions {

        public static IEnumerable< T > ForEach< T >( this IEnumerable< T > enumerable, Action< T, int > action )
        {
            int i = 0; 
            foreach ( var e in enumerable ) {
                action.Invoke( e, i++ );
            }
            return enumerable;
        }

        public static IEnumerable< T > Randomize< T >( this IEnumerable<T> source )
        {
            Random rnd = new Random();
            return source.OrderBy< T, int >( ( item ) => rnd.Next() );
        }

    }

}

