using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using System.Collections;

namespace Cyberevolver.Unity
{
    public class MonoBehaviourPlus : MonoBehaviour
    {




        private static readonly BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        private IEnumerable<PropertyInfo> GetAllRequirer() => this.GetType().GetProperties(flags).Where(item => item.GetCustomAttribute<AutoAttribute>() != null);
        protected virtual void Awake()
        {
            foreach (PropertyInfo prop in GetAllRequirer())
            {
                PropertyInfo trulyProp = prop.DeclaringType.GetProperty(prop.Name,flags);
                trulyProp.SetValue(this, this.gameObject.TryGetElseAdd(prop.PropertyType) );

            }
             
           
             
        }
       
      

        //without this, invoking extension requirer using "this"

        public MethodDelay InvokeRepeating(Action<MonoBehaviour> action, TimeSpan time, int limit = -1)
            => MonoBehaviourExtension.InvokeRepeating(this,action, time, limit);
        public  MethodDelay InvokeRepeating(Action<MonoBehaviour> action, float time, int limit = -1)
           => MonoBehaviourExtension.InvokeRepeating(this,action, time, limit);
        public  MethodDelay Invoke(Action<MonoBehaviour> action, TimeSpan time)
              => MonoBehaviourExtension.Invoke(this, action, time);
        public  MethodDelay Invoke( Action<MonoBehaviour> action, float time)
            => MonoBehaviourExtension.Invoke(this, action, time);
      
        #region Deactived
        [Obsolete("", true)]
        protected new void Invoke(string methodName, float time) { }
        [Obsolete("", true)]
        protected new void InvokeRepeating(string methodName, float time, float repateRat) { }
      
        #endregion

    }
    public static class MonoBehaviourExtension
    {
        public static MethodDelay InvokeRepeating(this MonoBehaviour mono,Action<MonoBehaviour> action, TimeSpan time, int limit = -1)
        {
            MethodDelay delay = new MethodDelay(limit, mono, time, action);
            delay.Run();
            return delay;
        }
        public static MethodDelay InvokeRepeating(this MonoBehaviour mono,Action<MonoBehaviour> action, float time, int limit = -1)
        {
            return mono.InvokeRepeating(action, TimeSpan.FromSeconds(time), limit);
        }

        public static MethodDelay Invoke(this MonoBehaviour mono,Action<MonoBehaviour> action, TimeSpan time)
        {
            MethodDelay delay = new MethodDelay(mono
                , time, action);
            delay.Run();
            return delay;
        }
        public static MethodDelay Invoke(this MonoBehaviour mono,Action<MonoBehaviour> action, float time)
        {
            return mono.Invoke(action, TimeSpan.FromSeconds(time));
        }
    }


}
