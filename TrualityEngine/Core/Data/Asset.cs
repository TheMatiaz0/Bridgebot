namespace TrualityEngine.Core
{




    public class Asset<T> : BaseAsset, IAsset<T>
        where T : class
    {
        public new T Value => (T)base.Value;
        protected Asset(T val, string name) : base(val, name)
        {

        }
        public Asset() : base(null, null)
        {

        }
        public static Asset<T> CreateAnonymous(T val)
        {
            return new Asset<T>(val, null);
        }
        public new static Asset<T> Get(string name)
        {
            return new Asset<T>((T)BaseAsset.Get(name).Value, name);
        }
        public  static Asset<T> Add(T val, string name)
        {
            return new Asset<T>((T)BaseAsset.Add(val, name).Value, name);
        }
        public static implicit operator Asset<T>(T value)
            => Asset<T>.CreateAnonymous(value);


    }
}
