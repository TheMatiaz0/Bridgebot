using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{

    /// <summary>
    /// Represent static <see cref="Entity"/> which has one static Main instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MainEntity<T>:Entity,IMainEntity
        where T:MainEntity<T>
    {

        /// <summary>
        /// If new scene is loading, last main entity will kiled and Game will create new
        /// </summary>
        public static T Main { get; protected set; }
        public  bool IsMain => Main == this;

        Entity IMainEntity.Main => Main;



        /// <summary>
        /// This make last main to normal, and select to main
        /// This will not killed last entity
        /// </summary>
        public virtual T SetToMain()
        {
            if (Main != null)
                Main.OnBeforeKilling.Value -= Main_OnBeforeKilling;
            Main = this as T;
            Main.OnBeforeKilling.Value += Main_OnBeforeKilling; ;
            return Main;
        }

        protected void Main_OnBeforeKilling(object sender,BaseBehaviourArgs args)
        {
            Main.OnBeforeKilling.Value -= Main_OnBeforeKilling;
            Main = null;
        }

        Entity IMainEntity.SetToMain()
        {
            return SetToMain();
        }
    }
}
