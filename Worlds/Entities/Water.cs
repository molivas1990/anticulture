using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Worlds.Entities
{
    public class Water : Entity
    {
        #region Species stuff
        public new static readonly Species Species;

        static Water()
        {
            Species = new Species("water", "glouglou", Factory);
            Species.Properties["weight"] = float.MaxValue;
            Species.Properties["hydration"] = 0.8f;
        }

        private static Entity Factory(World world) { return new Water(); }
        #endregion

        public Water()
            : base(Species)
        {
            Integrity = 20.0f;
        }
    }
}