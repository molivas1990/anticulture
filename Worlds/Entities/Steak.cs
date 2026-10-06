using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Worlds.Entities
{
    public class Steak : Entity
    {
        #region Species stuff
        public new static readonly Species Species;

        static Steak()
        {
            Species = new Species("steak", "matatou", Factory);
            Species.Properties["weight"] = 6.0f;
            Species.Properties["nutrition"] = 0.8f;
            Species.Properties["healing"] = -0.05f;
            Species.Properties["hydration"] = 0.1f;
        }

        private static Entity Factory(World world) { return new Steak(); }
        #endregion

        public Steak()
            : base(Species)
        {
            Integrity = 6.0f;
        }
    }
}
