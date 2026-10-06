using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Worlds.Entities
{
    public class Tree : Entity
    {
        #region Species stuff
        public new static readonly Species Species;

        static Tree()
        {
            Species = new Species("tree", "cuboire", Factory);
            Species.Properties["weight"] = 1000.0f;
            Species.Properties["anchored"] = 1.0f;
            Species.Properties["nutrition"] = 0.1f;
            Species.Properties["healing"] = -0.2f;
        }

        private static Entity Factory(World world) { return new Tree(); }
        #endregion

        public Tree()
            : base(Species)
        {
            Integrity = 100.0f;
        }
    }
}
