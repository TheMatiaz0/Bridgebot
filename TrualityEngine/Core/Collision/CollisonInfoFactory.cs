using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

//namespace TrualityEngine.Core
//{
//   /// <summary>
//   /// Static factory
//   /// </summary>
//    public static class CollisionInfoFactory
//    {
//        /// <summary>
//        /// Creating <see cref="ICollisionInfo"/ from base structure>
//        /// </summary>
//        public static ICollisionInfo Create<T>(T valueType)
//            where T : struct
//        {
//            if (valueType is Rectangle)
//                return new RectInfo((Rectangle)((ValueType)valueType));
//            else if (valueType is Line)
//                return new LineInfo((Line)(ValueType)valueType);
//            else if (valueType is Circle)
//                return new CircleInfo((Circle)(ValueType)valueType);
//            else throw new ArgumentException();

//        }
//        /// <summary>
//        /// Creating <see cref="ICollisionInfo"/ from base structure>
//        /// </summary>
//        public static IEnumerable<ICollisionInfo> Create<T>(IEnumerable<T> valueTypes)
//            where T : struct
//        {
//            return valueTypes.Select(item => CollisionInfoFactory.Create(item));
//        }
//    }
//}
