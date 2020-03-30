using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
    public class PhysicsManager : IEntityManager<ColliderEntity>
    {
        public bool IsActive { get; set; } = true;
        public bool StrongVelocity { get; set; } = false;
        public ColliderEntity Entity { get; private set; }
        public Vect2 Gravity { get; set; }

        public BodyType BodyType { get; set; } = BodyType.Base;

        public Vect2 Velocity { get; set; } = Vect2.Zero;
        public PhysicsManager(ColliderEntity entity)
        {
            Entity = entity;
        }
       
        internal void FixedUpdate()
        {
            if (IsActive == false)
                return;
            Velocity += Gravity;
            if (this.BodyType!=BodyType.Static && Velocity != Vect2.Zero)
            {
                if(Move(Velocity) && StrongVelocity == false)
                {
                    //third dynamic rule here:
                    Vect2 norm = Velocity.Normalized;
                    Velocity -= Velocity.Lenght > 1 ? norm : Velocity;
                }

            }  
            
        }

        public bool TestMove(Vect2 vect)
        {

            Vect2 previous = Entity.Pos;

            bool output = this.Move(vect);

            Entity.Pos = previous;

            return output;
        }

        public bool Move(Vect2 vect)
        {
            
            if (IsActive == false||BodyType==BodyType.Static)
                return false;
            

          

            Entity.Pos += vect;

            var entities = Entity.ColliderManager.GetCollisionsWithAliveEntities();

            bool result = false;
            foreach (ColliderEntity other in entities)
            {
                this.Entity.ColliderManager.RegisterCol(other);
                other.ColliderManager.RegisterCol(Entity);
                if (other.Physics.BodyType == BodyType.Trigger)
                    continue;
               
                result = true;

                if (    (other.Physics.BodyType == BodyType.Base)
                    &&  (this.BodyType == BodyType.Base)) 
                {
                       
                    Vect2 overall = this.Velocity + other.Physics.Velocity;
                    Entity.Pos -= overall;
                    other.Pos += overall;
                    
                }
                if (    (this.BodyType == BodyType.Strong)
                     && (other.Physics.BodyType == BodyType.Strong)) 
                {


                }
                else if (   (other.Physics.BodyType == BodyType.Static)
                        || (other.Physics.BodyType == BodyType.Strong)) 
                {

                    Entity.Pos -= vect;

                }
                else if (   (this.BodyType == BodyType.Strong)
                        && (other.Physics.BodyType == BodyType.Base))
                {
                    
                   
                    other.Pos += vect;


                }


            }
            return result;
            
            






        }


    }
}
