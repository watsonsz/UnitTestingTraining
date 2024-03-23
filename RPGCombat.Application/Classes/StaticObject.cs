using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public class StaticObject: BaseClass
    {
        public StaticObject()
        {
            _health = 2000;
        }
        public string Name { get; set; }
        public bool IsDestroyed { get; set; } = false;
        private double _health;
        
        public override double Health
        {
            get => _health;
            set
            {
                _health = value;
                if(_health <= 0)
                {
                    Destroy();
                }
            }

        }



        public event EventHandler DestroyRequested;

        private void Destroy()
        {
            IsDestroyed = true;
            OnDestroyRequested();
        }
        protected virtual void OnDestroyRequested()
        {
            DestroyRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
