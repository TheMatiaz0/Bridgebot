using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IniParser.Parser;
using IniParser.Model;
using System.IO;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Object, which you can kill
    /// </summary>
    public abstract class EObject : IKillable
    {
        public static EObject StaticMachine { get; } = new StaticM();
        private class StaticM : EObject
        {
            public override bool IsKill { get; protected set; }

            public override bool IsActive => true;

            public override void Kill()
            {
                throw new NotImplementedException();
            }
           
        }

        private static int IdQuanity { get; set; }
        /// <summary>
        /// This YieldUpater will be stopped when object die.
        /// </summary>
        public YieldUpdater  Yield { get; private set; }
        public int Id { get; private set; }
        public abstract void Kill();
        public abstract bool IsKill { get; protected  set; }
        public abstract bool IsActive { get; }

      

        public Tween InvokeWithDelay(Action action,TimeSpan timeSpan)
        {
            return new Tween(timeSpan,this).SetOnEnd(action).Start();
        }
        public Tween InvokeRepeating(Action action,TimeSpan timeSpan,int amount)
        {
            return new Tween(timeSpan,this).SetOnEnd(action).SetRepeat(amount).Start();
        }
        public Tween InvokeInNextFrame(Action action)
        {
            return new Tween(TimeSpan.Zero,this).SetOnEnd(action).Start();
        }
        public Tween InvokeAfterTwoFrame(Action action)
        {
            return new Tween(TimeSpan.Zero, this).SetOnEnd(() => new Tween(TimeSpan.Zero, this).SetOnEnd(action).Start()).Start();
        }
       

        public EObject()
        {
            Id = IdQuanity++;
            Yield = new YieldUpdater(this);

        }
     
        public void LoadIni(string file)
        {
            IniData result;
            if (File.Exists(file) == false)
            {
                
            }
            using (StreamReader reader = new StreamReader(file))
            {
                IniParser.Parser.IniDataParser p = new IniDataParser();
                result = p.Parse(reader.ReadToEnd());

            }
            foreach (PropertyInfo prop in
                this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance|BindingFlags.Instance).Where(i => i.GetCustomAttribute<IniPropAttribute>() != null))
            {
                if(result.TryGetKey(prop.Name,out string iniCode))
                {
                    prop.SetValue(this, IniManager.LoadSingleObject(iniCode, prop.PropertyType));
                }
              
            }
       
        }
        internal static void UpdateStaticMachine()
        {
            (StaticMachine as StaticM).Yield.Update();
        }


    }
}
