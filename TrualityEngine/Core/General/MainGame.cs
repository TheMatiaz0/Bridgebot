using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Your main game logic
    /// </summary>
    public class GameHeart
    {
        private static Dictionary<Keys,MethodInfo> staticBinds;
        private static Dictionary<Type, Pair<Keys, MethodInfo>[]> sceneBinds;
        public static GameHeart Actual { get; internal set; }
        public string GameName { get; protected set; }
        /// <summary>
        /// Base XNA game
        /// </summary>
        public TheGame BaseGame { get; set; }

        /// <summary>
        /// Put this into your initialization code.
        /// It will be called first.
        /// </summary>
        public virtual void Initialization() 
        {
#if DEBUG
            staticBinds = TheReflection.GetAllStaticMethod(m => m.GetParameters().Length == 0, typeof(DebugBindAttribute))
                .ToDictionary(m => m.GetCustomAttribute<DebugBindAttribute>().Key);

            sceneBinds = TheReflection.GetAllMethod(m => m.IsStatic == false && m.DeclaringType.IsSubclassOf(typeof(SceneBehaviour)), typeof(DebugBindAttribute))
                .Select(item => new Pair<Keys, MethodInfo>(item.GetCustomAttribute<DebugBindAttribute>().Key, item))
                .GroupBy(item => item.Second.DeclaringType).ToDictionary(item => item.Key, item => item.ToArray());

            foreach (var item in TheReflection.GetAllType(null)
                .SelectMany(item => item.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic))
                .Where(item => item.GetCustomAttribute<LoadOnInitializeAttribute>() != null))
                item.Invoke(null, null);

               
#endif
        }
        /// <summary>
        /// Put this into your content load.
        /// It will be called after <c>GameHear.Initialization()</c>
        /// </summary>
        public virtual void LoadContent() { }
        /// <summary>
        /// Put this into your unload content code
        /// </summary>
        public virtual void Unload() { }
      
        /// <summary>
        /// Main start game logic.
        /// It will be called before first Update.
        /// You should load start of scene in this method.
        /// </summary>
        public virtual void Start() { }
        /// <summary>
        /// Main update game logic.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) 
        {
#if DEBUG
            foreach (var element in staticBinds)
                if (Input.Actual.IsPressed(element.Key))
                    element.Value.Invoke(null,null);
            if (sceneBinds.ContainsKey(SceneRuntime.ActualScene.ScenePrefix.GetType()))
                foreach (Pair<Keys, MethodInfo> element in sceneBinds[SceneRuntime.ActualScene.ScenePrefix.GetType()])
                    if (Input.Actual.IsPressed(element.First))
                        element.Second.Invoke(SceneRuntime.ActualScene.ScenePrefix, null);
#endif
            EObject.UpdateStaticMachine();
        }    

        /// <summary>
        /// Put this into your close game code
        /// </summary>
        public virtual void OnClose() { }
    }
}
