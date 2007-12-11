using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Worlds.Operations
{
    public class TellFromExperience : Operation
    {
        #region Static operator
        public static readonly Operator Operator = new Operator("tellfromexperience", Factory, 1);

        private static Operation Factory(Human who, Entity[] what)
        {
            if (what.Length != 1) throw new ArgumentException("TellFromExperience takes a single argument");

            // Don't do it if the target is too far
            if (Vector.Distance(who.Position, what[0].Position) > 2.0f) return null;
            // Must be human
            if (what[0].Species != Human.Species) return null;

            // Find if there's something to be said
            Human.Need bestNeed = null;
            Human.Need.Solution bestSolution = null;
            
            
           
            // Takes a random need
            Random MyRandom = new Random();
            Human.Need[] Needs = who.Needs.ToArray();
            Human.Need need = Needs[MyRandom.Next(Needs.Length)];
            

            //Find best solution for it
            foreach (Human.Need.Solution solution in need.Solutions)
            {
                if (bestSolution == null)
                {
                    bestNeed = need;
                    bestSolution = solution;
                }
                else if (solution.Effectiveness < bestSolution.Effectiveness)
                {
                    bestNeed = need;
                    bestSolution = solution;
                }
            }
            

            if (bestSolution == null) return null;

            return new TellFromExperience(who, what[0] as Human, bestNeed, bestSolution);
        }
        #endregion

        #region Data members
        private Human mWho;
        private Human mWhat;
        private Human.Need mBestNeed;
        private Human.Need.Solution mBestSolution;
        private float mTimeLeft = 2.0f;
        #endregion

        #region Constructor
        public TellFromExperience(Human who, Human what, Human.Need bestNeed, Human.Need.Solution bestSolution)
        {
            mWho = who;
            mWhat = what;
            mBestNeed = bestNeed;
            mBestSolution = bestSolution;
        }
        #endregion

        #region Properties
        public override OperationPrototype Prototype
        {
            get { return new OperationPrototype(Operator, new Species[] { Human.Species }); }
        }

        public override bool IsOver
        {
            get { return mTimeLeft <= 0.0f; }
        }
        #endregion

        #region Overriden methods
        public override void Update(Timer timer, Random random)
        {
            float ExperienceValue;
            if (mWhat.Age >= 1)
                ExperienceValue = mWho.Age / mWhat.Age;
            else
                ExperienceValue = mWho.Age;

            
            

            mTimeLeft -= timer.TimeDelta;
            if (mTimeLeft <= 0.0f)
            {
                mWho.Stimulate("SocialCohesion", -timer.TimeDelta);

                Human.Need need = mWhat.FindNeed(mBestNeed.Name);
                bool found = false;
                foreach (Human.Need.Solution solution in need.Solutions)
                {
                    if (solution.Prototype == mBestSolution.Prototype)
                    {
                        mBestSolution.Effectiveness = solution.Effectiveness * ExperienceValue;
                        found = true;
                    }
                }
                if (!found)
                    need.Solutions.Add(new Human.Need.Solution(mBestSolution.Prototype, mBestSolution.Effectiveness * ExperienceValue));
            }
        }

        public override string ToString()
        {
            string message = mWhat.Name + "! when you need " + mBestNeed.Name + ", " + mBestSolution.Prototype.Operator.Name;
            if (mBestSolution.Prototype.Operands != null)
            {
                foreach (Species species in mBestSolution.Prototype.Operands)
                {
                    message += " " + species.Name;
                }
            }
            return message;
        }
        #endregion
    }
}
