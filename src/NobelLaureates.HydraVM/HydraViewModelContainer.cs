using System;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.HydraVM
{
    public abstract class HydraViewModelContainer
    {
        private List<IHydraBehaviour> _behaviours = new List<IHydraBehaviour>();

        public virtual void Start()
        {
            lock (_behaviours)
            {
                var behaviours = _behaviours.ToList();
                behaviours.ForEach(b => b.Start());
            }
        }

        public virtual void Stop()
        {
            lock (_behaviours)
            {
                var behaviours = _behaviours.ToList();
                behaviours.ForEach(b => b.Start());

            }
        }

        public HydraViewModelContainer AddBehaviour(IHydraBehaviour behaviour)
        {
            if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

            lock (_behaviours)
            {
                _behaviours.Add(behaviour);
            }
            return this;
        }

        public HydraViewModelContainer AddBehaviours(IHydraBehaviour[] behaviours)
        {
            if (behaviours == null) throw new ArgumentNullException(nameof(behaviours));

            lock (_behaviours)
            {
                _behaviours.AddRange(behaviours);
            }
            return this;
        }
    }
}
