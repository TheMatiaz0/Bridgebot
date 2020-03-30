using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// ColliderLayer  is one from two options to lock collision with not important object.
    /// It accept only concret <see cref="ColliderLayer"/> as Collider.It should be set on Init in <see cref="GameHeart"/>
    /// </summary>
    public class ColliderLayer
    {
        /// <summary>
        /// Flags is related with point colliding test. If point colliding test include flags which is in it,colliding test will defintely return false.
        /// It cannot be set outside <see cref="GameHeart"/> init.
        /// </summary>
        public PointFlags DontAcceptFlags
        {
            get => _DontAcceptFlags;
            set
            {
                InitTest();
                _DontAcceptFlags = value;
            }
        }
        /// <summary>
        ///  It isn't possible to two layer with identical name exist
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Base layer colliding with anything
        /// </summary>
        public static readonly ColliderLayer Base;
        private static bool deactiveInitTest = false;
        private bool _AcceptTheSameLayer = true;
        /// <summary>
        /// Declaring if you want the same layer colliding
        /// </summary>
        public bool AcceptTheSameLayer
        {
            get => _AcceptTheSameLayer;
            set
            {
                InitTest();
                _AcceptTheSameLayer = value;
            }
        }
        static ColliderLayer()
        {

            object o = new object();
            lock (o)
            {
                deactiveInitTest = true;
                Base = new ColliderLayer("Base");
                Base.AcceptAllLayer();
                deactiveInitTest = false;
            }

        }
        private bool acceptAll = false;
        private List<ColliderLayer> actualAcceptable = new List<ColliderLayer>();
        private PointFlags _DontAcceptFlags;
        private readonly static Dictionary<string, ColliderLayer> all = new Dictionary<string, ColliderLayer>();
        private ColliderLayer(string name)
        {
            Name = name;
            all.Add(name, this);
        }
        /// <summary>
        /// Accept all other layer and yourself.
        /// It cannot be use outside <see cref="GameHeart"/> init.
        /// </summary>
        public void AcceptAllLayer()
        {
            InitTest();
            acceptAll = true;
            AcceptTheSameLayer = true;
            actualAcceptable.Clear();

        }
        private static void InitTest()
        {
            if (deactiveInitTest == false && TheGame.Instance.IsIniting == false)
                throw new Exception("Cannot set this outside from game heart init");
        }
        /// <summary>
        /// Remove conret layer accept. If you want remove accept yourself you should use <see cref="AcceptTheSameLayer"/>.
        /// It cannot be use outside <see cref="GameHeart"/> init.
        /// </summary>
        /// <param name="colliderLayer"></param>
        /// <returns></returns>
        public bool RemoveAcceptLayer(ColliderLayer colliderLayer)
        {
            InitTest();
            if (acceptAll)
            {
                acceptAll = false;
                actualAcceptable = all.Where(item=>item.Value!=colliderLayer).Select(item => item.Value).ToList();
            }

            return actualAcceptable.Remove(colliderLayer);
        }
        /// <summary>
        /// After invoke it, layer cannot colliding with anything
        /// It cannot be use outside <see cref="GameHeart"/> init.
        /// </summary>
        public void RemoveAllLayerAccept()
        {
            InitTest();
            acceptAll = false;
            AcceptTheSameLayer = false;
            actualAcceptable.Clear();

        }
        /// <summary>
        /// Returns true if layer can colliding with other.
        /// If <c>a.IsGood(b) equals true , doesn't mean b.IsGood(a) is true either</c>
        /// </summary>
        /// <param name="colliderLayer">Layer wich you want check if this can colliding with this</param>
        /// <returns></returns>
        public bool IsGood(ColliderLayer colliderLayer)
        {
            return acceptAll || (colliderLayer == this && this.AcceptTheSameLayer) || this.actualAcceptable.Any(item => item == colliderLayer);
        }
        /// <summary>
        /// Checking if collider can colliding with concret flags
        /// </summary>
        /// <returns> Returns true if layer can colliding with this point flag</returns>
        public bool IsGood( PointFlags flags)
        {
            return ((DontAcceptFlags & flags) == PointFlags.None);
             

        }
        /// <summary>
        /// Adding layer. You should set which layer can collision. 
        /// It isn't possible to two layer with identical name exist.
        /// Name:"Base" is always lock.
        /// It cannot be use outside <see cref="GameHeart"/> init.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ColliderLayer Create(string name)
        {
            InitTest();
            if (all.ContainsKey(name))
                throw new ArgumentException("This layer has already exit, use Get()");
            return new ColliderLayer(name);

        }
        /// <summary>
        /// Getting layer using name. Good practice is save layer in field when you initalize them in GameHeart
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns equals layer.If two layer have identical  name, they also be reference equal</returns>
        public static ColliderLayer Get(string name)
        {
            if (all.ContainsKey(name) == false)
                return null;
            return all[name];
        }
    }
}
