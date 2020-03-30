using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public struct Raycast
    {
       

        public Vect2 Start { get; set; }
        public Direction Dir { get; set; }
        public float Lenght { get; set; }
     
        public Raycast(Vect2 start, Direction dir, float lenght)
        {
            Start = start;
            Dir = dir;
            Lenght = lenght;
        }


        public static ColliderEntity CathFirst(Raycast ray)
            => ray.CathFirst();
        public static IEnumerable<ColliderEntity> CathAll(Raycast ray)
            =>ray.Cath();

        public ColliderEntity CathFirst()
        {
            Vect2 start = Start;
            var list = Cath().ToList();
            if (list.Count == 0)
                return null;
            list.Sort((a, b) => Vect2.Distance(start, a.Pos).CompareTo(Vect2.Distance(start, b.Pos)));
            return list[list.Count - 1];
        }
        public IEnumerable<ColliderEntity> Cath()
        {
            ColliderEntity[] entities = Entity.GetEntities().Where(item => item is ColliderEntity c && c.ColliderManager.IsActive)
                .Select(item => item as ColliderEntity)
                .ToArray();
            foreach(var item in entities)
            {
                Line line = new Line(Start,Start+ Dir*Lenght);
                if (item.GetCollision().IsTouch(new Collider(line)))
                    yield return item;
                
            }
            yield break;



        }

       

    }
}
