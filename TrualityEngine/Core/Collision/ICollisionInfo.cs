using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Represent abstract collison,which can be return by <see cref="Entity"/> or his inheritance
    /// </summary>
    public interface ICollisionInfo
    {

        bool Intersects(ICollisionInfo collisionInfo);
        bool Contains(Vect2 pos);
        
    }
}
