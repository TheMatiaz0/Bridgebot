using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class BulletManager : AutoInstanceBehaviour<BulletManager>
{
	[SerializeField,AssetOnly]
	private Bullet defaultBullet = null, enemyBullet = null;

   

  
    public Bullet DefaultBullet => defaultBullet;
    public Bullet EnemyBullet => enemyBullet; 
	public Bullet Shoot(float speed, uint dmg, Direction dir, Vector2 from, Team team, Bullet prefab = null,
        Action<Bullet> onDestroy=null)
	{
		if (prefab == null)
			prefab = team == Team.Good ? DefaultBullet : EnemyBullet;
		Bullet instance = Instantiate(prefab);
		instance.Team = team;
		instance.Speed = speed;
		instance.Direction = dir;
		instance.transform.position = from;
		instance.Dmg = dmg;
        if (onDestroy != null)
            instance.OnDestroy += (s, e) => onDestroy(e.Value);
		return instance;

	}
    public Bullet[] TrippleShoot(float speed, uint dmg, Direction dir, Vector2 from, Team team,float rangeBeetwenSidesShot, Bullet prefab = null,
        Action<Bullet> onDestroy = null)
    {

        Bullet[] bullets = new Bullet[3];
        Bullet SingleShoot(Direction direction,Vector2 pos)
        {
            return Shoot(speed, dmg, direction, pos, team, prefab, onDestroy);
        }
        Vector2 changer;
        if (Math.Abs(dir.X) < Math.Abs(dir.Y))
        {
            changer = new Vector2(1, 0);

        }
        else
        {
            changer = new Vector2(0, 1);
        }

        bullets[0]= SingleShoot(dir, from + changer * rangeBeetwenSidesShot);
        bullets[1]= SingleShoot(dir, from);
        bullets[2]= SingleShoot(dir, from - changer * rangeBeetwenSidesShot);
        return bullets;


    }
   


}