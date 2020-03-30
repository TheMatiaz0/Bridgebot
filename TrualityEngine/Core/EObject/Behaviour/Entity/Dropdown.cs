using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using TrualityEngine.Core;
using System.Collections.ObjectModel;
namespace TrualityEngine.Core
{
    public class Dropdown<T> : Entity
    {
        private class DropButton : Button
        {
            public DropdownElement<T> Element { get; set; }
   
            protected override void IfKill()
            {
                base.IfKill();
                if(Element.ButtonItself==this)
                {
                    Element.ButtonItself = null;
                }


            }
        }

        private ObservableCollection<DropdownElement<T>> elements = new ObservableCollection<DropdownElement<T>>();
        public bool IsExpand { get; private set; }
        public Button MainButton { get; }
        public bool CanExpandByClickButton { get; set; } = true;
        public Entity Port { get; }

        private DropdownElement<T> _Current;
        public DropdownElement<T> Current
        {
            get => _Current;
            set
            {
                if (Object.Equals(_Current, value))
                    return;
                var last = _Current;
                _Current = value;
                OnCurrentDropdownElementChanged.Invoke(this, new ChangeCurrentDropdownEventArgs<T>(this, last, _Current));
                if (_Current == null)
                    return;
                MainButton.TextEntity.Text = Current.Value?.ToString() ?? "null";


            }
        }
        public Event<ChangeCurrentDropdownEventArgs<T>> OnCurrentDropdownElementChanged { get; protected set; } = Event.Empty;


        public Dropdown()
        {
            this.AddChild(Port = new Entity() { OwnPos = Vect2.Zero });
            MainButton = PrefabCollection.GetClassicButton<Button>("", Color.White, Color.Black);
            MainButton.Entity.Pos = this.Pos;
            this.AddChild(MainButton.Entity);
            MainButton.OnClick.Value += OnMainButtonClick;
            elements.CollectionChanged += Elements_CollectionChanged;


        }

        private void Elements_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (IsExpand)
            {
                Expand();
            }
        }

        public DropdownElement<T> Add(T element, Action<Dropdown<T>, DropdownElement<T>> onClick = null)
        {
            DropdownElement<T> res;
            elements.Add(res = new DropdownElement<T>(element, onClick, this));
            return res;
        }
        public IEnumerable<DropdownElement<T>> AddRange(IEnumerable<T> elements, Action<Dropdown<T>, DropdownElement<T>> onClick = null)
        {
            if (elements is null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            foreach (var item in elements)
            {
                yield return Add(item, onClick);
            }
        }
      
        public bool Remove(DropdownElement<T> element)
        {
            return elements.Remove(element);
        }
        public bool Remove(T element)
        {
            return elements.Remove(elements.FirstOrDefault(item => Object.Equals(item.Value, element)));
        }


        private void OnMainButtonClick(object sender, SimpleArgs<Button> e)
        {
            if (CanExpandByClickButton == false)
                return;
            if (IsExpand == false)
                Expand();
            else
                Unexpand();
        }
        public void Expand()
        {

            if (IsExpand)
                Unexpand();
            IsExpand = true;
            int index = 0;
            foreach (var item in elements)
            {
                index++;
                DropButton b = PrefabCollection.GetClassicButton<DropButton>(item.Value?.ToString() ?? "null", Color.White, Color.Black);
                item.ButtonItself = b;
                Port.AddChild(b.Entity);
                b.OnClick.Value += OnClickButton;
                b.Element = item;
                b.Entity.Pos = this.Pos - new Vect2(0, b.Entity.FullScale.Y * (index));

            }
        }

        private void OnClickButton(object sender, SimpleArgs<Button> e)
        {
            DropButton b = sender as DropButton;
            Current = b.Element;
            b.Element.InvokeOnClick();
        }

        public void Unexpand()
        {
            foreach (Entity en in Port.GetChilds().ToArray()) en.Kill();
            IsExpand = false;
        }
    }

}

