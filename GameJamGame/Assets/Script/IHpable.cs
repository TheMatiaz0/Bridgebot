using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHpable
{
     Team CurrentTeam { get; }
     Hp Hp { get; }
}