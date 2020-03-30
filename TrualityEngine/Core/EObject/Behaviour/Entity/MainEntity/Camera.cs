using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
  
    public class Camera:MainEntity<Camera>
    {
       
      
        /// <summary>
        /// What is rendering in background
        /// </summary>
        public Color Background { get; set; }
     

        public Camera(Color? background=null)
        {
            Background = background ?? Color.Black;
        }
      

        public bool WillBeRender(ICollisionInfo collider)
        {
            return GetRect().Intersects(collider);
        }
        public Rect GetRect()
        {
            return new Rect(this.Pos.ToPoint() - (Screen.Virtual / 2).ToPoint(), Screen.Virtual.ToPoint());
        }
        public bool WillBeRender(Collider collider)
        {
            return collider.CollisionInfos.Any(colInfo => WillBeRender(colInfo));
        }
        /// <summary>
        /// Get center point of camera
        /// </summary>
        /// <returns></returns>
        public Vect2 GetCenter()
        {
            return new Vect2(this.Pos.X, this.Pos.Y);
        }
        internal Matrix GetMatrix()
        {
            return Matrix.CreateTranslation(-this.Pos.X, this.Pos.Y, 0);
        }
       
    }
}
