using Cyberevolver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Hp
{
	public enum Action
	{
		Add,
		Take
	}
	public class HpChangedArgs : EventArgs, ICloneable
	{
		public HpChangedArgs(string who, Cint last, Cint actual, Action action)
		{
			Who = who ?? throw new ArgumentNullException(nameof(who));
			Last = last;
			Actual = actual;
			Action = action;
		}

		public string Who { get; }
		public Cint Last { get; }
		public Cint Actual { get; }
		public Action Action { get; }
		public HpChangedArgs Clone()
		{
			return (HpChangedArgs)this.MemberwiseClone();
		}
		object ICloneable.Clone()
		{
			return Clone();
		}
	}
	private Cint _Value;
	private readonly List<string> effectors = new List<string>();
	public ReadOnlyCollection<string> Effectors => effectors.AsReadOnly();
	public Cint Value
	{
		get => _Value;
		private set
		{
			if (value == _Value)
				return;
			if (Max < value)
				_Value = Max;
			else if (Min > value)
				_Value = Min;
			else
				_Value = value;


		}
	}
	private void SetValue(Cint val, string who, Action action)
	{
        if(CanChange?.Invoke()??true)
        {
            effectors.Add(who);
            Cint last = Value;
            Value = val;
            var ev = new HpChangedArgs(who, last, Value, action);
            OnValueChanged(this, ev.Clone());
            if (Value == Min)
                OnValueChangeToMin(this, ev.Clone());
            switch (action)
            {
                case Action.Add:
                    OnHpGiven(this, ev.Clone()); break;
                case Action.Take:
                    OnHpTaken(this, ev.Clone()); break;
            }
        }
		
	}
	public string GetLastEffector() => (Effectors.Count >= 1) ? Effectors[Effectors.Count - 1] : null;
	public void TakeHp(Cint value, string who)
	{

		SetValue(Value - value, who, Action.Take);
	}

	public void GiveHp(Cint value, string who)
	{

		SetValue(Value + value, who, Action.Add);
	}

	public event EventHandler<HpChangedArgs> OnValueChanged = delegate { };
	public event EventHandler<HpChangedArgs> OnHpTaken = delegate { };
	public event EventHandler<HpChangedArgs> OnHpGiven = delegate { };
	public event EventHandler<HpChangedArgs> OnValueChangeToMin = delegate { };

	public bool IsInRange(float val)
		=> Max >= val && Min <= val;
	public uint Max { get; }
	public uint Min { get; }
    public Func<bool> CanChange { get; }

	public Hp(uint max, uint min, uint value,Func<bool> canChange=null)
	{
		Max = max;
		Min = min;
		Value = value;
        CanChange = canChange;
	}
	public override string ToString()
	{
		return $"{Value}/{Max}";
	}



}