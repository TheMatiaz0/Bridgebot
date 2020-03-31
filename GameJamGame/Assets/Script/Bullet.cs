using Cyberevolver;
using Cyberevolver.Unity;
using System;

using UnityEngine;
public enum Team
{
    Good,Bad
}

public class Bullet : MonoBehaviourPlus
{


	[SerializeField]
	private GameObject effect = null;

	[Auto]
	public SpriteRenderer Render { get; set; }
	public float Speed { get; set; }
	public Cint Dmg { get; set; }
	public Team Team { get; set; }
	public Direction Direction { get; set; }
	public TimeSpan LifeTime { get; private set; }
    public EventHandler<SimpleArgs<Bullet>> OnDestroy = delegate { };





    protected virtual void Update()
	{
		this.transform.position += (Vector3)(Speed * Direction.ToVector2() * Time.deltaTime);
		LifeTime += TimeSpan.FromSeconds(Time.deltaTime);
		if (LifeTime > TimeSpan.FromSeconds(10))
			Destroy(this.gameObject);
        this.transform.LookAt2D((Vector2)this.transform.position + Direction.ToVector2());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
	

	}
    private bool destroying = false;
	public  void Kill()
	{

        if (destroying)
            return;
        OnKill();
        OnDestroy.Invoke(this, this);
        destroying = true;
        if (effect != null)
            Instantiate(effect).transform.position = this.transform.position;
        Destroy(this.gameObject);
    }
    protected virtual void OnKill()
    {

    }

   
  
}


