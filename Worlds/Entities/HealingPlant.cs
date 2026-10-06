using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Worlds.Entities
{
    public class HealingPlant : Entity
    {
        #region Species stuff
        public new static readonly Species Species;

        static HealingPlant()
        {
            Species = new Species("healingplant", "srabis", Factory);
            Species.Properties["weight"] = 1.0f;
            Species.Properties["anchored"] = 1.0f;
            Species.Properties["nutrition"] = 0.1f;
            Species.Properties["healing"] = 0.2f;
            Species.Properties["hydration"] = 0.02f;
        }

        private static Entity Factory(World world) { return new HealingPlant(); }
        #endregion

        public HealingPlant()
            : base(Species)
        {
            Integrity = 3.0f;
        }
    }
}
