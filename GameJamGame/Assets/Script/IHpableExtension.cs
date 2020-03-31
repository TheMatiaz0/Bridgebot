using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class HpableExtension
{
    public static bool IsFromWrongTeam(this IHpable hpable,Collider2D colider,out Bullet bullet)
    {
       
        if ((bullet = colider.GetComponent<Bullet>())!=null)
            return bullet.Team != hpable.CurrentTeam;
        else
            return false;
                
    }
}