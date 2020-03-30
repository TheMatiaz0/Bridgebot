using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using TrualityEngine.Core;
namespace TrualityEngine.Core
{
    public abstract class SingletonDataContainer<TITself> : EObject
           where TITself : SingletonDataContainer<TITself>, new()
    {
        protected abstract string IniFileName { get; }
        public static TITself Current
        {
            get => _Current ?? (_Current = new TITself());

        }
        private static TITself _Current;
        public SingletonDataContainer()
        {
            LoadIni(IniFileName);
        }
        public override bool IsKill { get => false; protected set => throw new NotImplementedException(); }

        public override bool IsActive => true;

        public override void Kill()
        {

        }
    }
}

