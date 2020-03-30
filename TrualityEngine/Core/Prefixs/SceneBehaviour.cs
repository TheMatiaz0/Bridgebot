using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// This prefix will be used as scene logic.
   
    /// </summary>
    public class SceneBehaviour:IPrefix,IKillable
    {
        
        /// <summary>
        /// Runtime scene will be created if game is running
        /// </summary>
        public SceneRuntime Scene { get; set; } = null;
        /// <summary>
        /// Simplfies version Scene.Yield
        /// </summary>
        public YieldUpdater Yield => Scene.Yield;

        private List<Action> Creator { get; }
         public IReadOnlyList<Action> GetCreator()
        {
            return Creator.AsReadOnly();
        }
        /// <summary>
        /// Scene name
        /// </summary>
        public string Name { get; set; }
        private static int NoNamedQuanity { get; set; } = 0;

        bool IKillable.IsKill => Scene == null || Scene.IsKill;

        bool IKillable.IsActive => ((IKillable)this).IsKill == false && Scene.IsActive;

        public SceneBehaviour(string name=null , params Action[] creator)
        {
           
            Name = name ?? (++NoNamedQuanity).ToString();
            Creator = creator.ToList() ?? new List<Action>();
        }
        public IReadOnlyList<Action> GetEntitesFunc() => Creator;

        /// <summary>
        /// Call when scene is loaded
        /// </summary>
        public virtual void SceneLoaded( ) { }
     
        public virtual void Update( ) { }
        public virtual void BeforeClosingScene() { }
        public virtual void AfterClosingScene() { }
        /// <summary>
        /// Like update, but after update
        /// </summary>
        public virtual void LateUpdate() { }
        public virtual void Draw(FixedBatch batch) { }

       
    }
}
