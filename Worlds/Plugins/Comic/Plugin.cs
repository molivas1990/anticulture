using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AntiCulture.Worlds;

namespace AntiCulture.Worlds.Plugins.Comic
{
    public class Plugin : AntiCulture.Worlds.Plugin
    {
        private World mWorld;
        private Human mHuman;
        private FileStream mStream;
        private TextWriter mWriter;
        private uint mCellCount = 0;
        private string mPreviousText;

        public override string Name
        {
            get { return "Comic"; }
        }

        public override void Init(World world)
        {
            Console.WriteLine("Please enter the name of the human to be tracked : ");
            string humanName = Console.ReadLine();
            Human human = world.FindNamed(humanName) as Human;
            if (human == null) throw new Exception("Couldn't find human \"" + humanName + "\"");
            mWorld = world;
            mHuman = human;
            mHuman.OperationStarted += new OperationEventHandler(OperationStarted);
            mStream = new FileStream("comic.html", FileMode.Create, FileAccess.Write);
            mWriter = new StreamWriter(mStream);

            mWriter.Write("<html>\n");
            mWriter.Write("<html><head><title>AntiCulture.Worlds.Plugins.Comic output</title>\n");
            mWriter.Write("<style>.Rectangle{float:left;width:200px;border-style:solid;border-width:1px;border-color:black;padding:4px;margin:3px}</style>\n");
            mWriter.Write("</head><body>\n");
            mWriter.Write("<h1 align=\"center\">Les aventures de " + humanName + "</h1>\n");
        }

        void OperationStarted(object sender, OperationEventArgs e)
        {
            OperationPrototype prototype = e.Operation.Prototype;

            if ((mCellCount % 5) == 0) mWriter.Write("</tr>\n<tr>");
            mCellCount++;

            string CurrentText;
            CurrentText = e.Operation.ToString();

            if (CurrentText == mPreviousText)
                return;

            mWriter.Write("<div class=\"Rectangle\">");
            mWriter.Write("<img src=\"resources/entities/human.png\"/>");
            mWriter.Write("<img src=\"resources/operators/" + prototype.Operator.Name + ".png\"/>");
            if (prototype.Operands != null)
            {
                foreach (Species i in prototype.Operands)
                {
                    mWriter.Write("<img src=\"resources/entities/" + i.Name + ".png\"/>");
                }
            }
            mWriter.Write("<div>" + e.Operation.ToString() + "</div>");
            mWriter.Write("</div>\n");


            mPreviousText = CurrentText;
        }

        public override bool Tick()
        {
            return true;
        }

        public override void Shutdown()
        {
            mWriter.Write("</body></html>");
            mWriter.Dispose();
            mStream.Dispose();
        }
    }
}
