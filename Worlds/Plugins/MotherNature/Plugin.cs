using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AntiCulture.Worlds.Plugins.MotherNature
{
    internal class CanRenderFlag
    {
        public bool IsSet = false;
    }

    internal class ThreadStartParam
    {
        public CanRenderFlag CanRenderFlag;
        public World World;
    }

    public class Plugin : AntiCulture.Worlds.Plugin
    {
        private CanRenderFlag mCanRenderFlag = new CanRenderFlag();

        public override string Name
        {
            get { return "MotherNature"; }
        }

        public override void Init(World world)
        {
            ThreadStartParam param = new ThreadStartParam();
            param.World = world;
            param.CanRenderFlag = mCanRenderFlag;
        }



        public override bool Tick()
        {
            mCanRenderFlag.IsSet = true;
            while (mCanRenderFlag.IsSet)
                System.Threading.Thread.Sleep(10);
            return true;
        }

        public override void Shutdown()
        {
        }
    }
}
