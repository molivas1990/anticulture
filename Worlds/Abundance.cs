using System;
using System.Collections.Generic;

namespace AntiCulture.Worlds
{
    /// <summary>
    /// Keeps the meadow stocked with food, water, and plants.
    /// </summary>
    public class Abundance
    {
        #region Stock
        private struct Stock
        {
            public string Name;
            public int Minimum;

            public Stock(string name, int minimum)
            {
                Name = name;
                Minimum = minimum;
            }
        }

        // Enough that a person looking around (eyesight 10) is standing in food.
        private static readonly Stock[] Stocks = new Stock[]
        {
            new Stock("apple", 250),
            new Stock("water", 200),
            new Stock("tree", 80),
            new Stock("healingplant", 80),
            new Stock("plant", 100),
            new Stock("steak", 50),
            new Stock("stone", 60),
            new Stock("rock", 40),
        };

        public const float MeadowHalfExtent = 18.0f;
        private const float RestockInterval = 5.0f;
        private const int RestockBatch = 80;
        private const float FruitChance = 0.35f;
        #endregion

        #region Data members
        private float mTimeUntilRestock = RestockInterval;
        #endregion

        #region Methods
        public static Vector Scatter(Random random)
        {
            return new Vector(
                (float)random.NextDouble() * (MeadowHalfExtent * 2.0f) - MeadowHalfExtent,
                (float)random.NextDouble() * (MeadowHalfExtent * 2.0f) - MeadowHalfExtent);
        }

        public void Fill(World world, Random random)
        {
            foreach (Stock stock in Stocks)
                SpawnMissing(world, random, stock.Name, stock.Minimum, stock.Minimum);
        }

        public void Update(World world, Timer timer, Random random)
        {
            mTimeUntilRestock -= timer.TimeDelta;
            if (mTimeUntilRestock > 0.0f) return;
            mTimeUntilRestock = RestockInterval;

            foreach (Stock stock in Stocks)
                SpawnMissing(world, random, stock.Name, stock.Minimum, RestockBatch);

            DropFruit(world, random);
        }

        public static int CountLiving(World world, string speciesName)
        {
            int count = 0;
            foreach (Entity entity in world.Entities)
            {
                if (entity.IsAlive && entity.Species.Name.Equals(speciesName, StringComparison.InvariantCultureIgnoreCase))
                    ++count;
            }
            return count;
        }

        private static void SpawnMissing(World world, Random random, string speciesName, int minimum, int batch)
        {
            Species species = world.Encyclopedia.FindSpecies(speciesName);
            if (species == null || species.Factory == null) return;

            int missing = minimum - CountLiving(world, speciesName);
            if (missing <= 0) return;
            if (missing > batch) missing = batch;

            for (int i = 0; i < missing; ++i)
            {
                Entity entity = species.Factory(world);
                entity.Position = Scatter(random);
                world.Entities.Add(entity);
            }
        }

        private static void DropFruit(World world, Random random)
        {
            Species apple = world.Encyclopedia.FindSpecies("apple");
            if (apple == null || apple.Factory == null) return;

            int appleMinimum = 0;
            foreach (Stock stock in Stocks)
            {
                if (stock.Name == "apple")
                {
                    appleMinimum = stock.Minimum;
                    break;
                }
            }

            int apples = CountLiving(world, "apple");
            if (apples >= appleMinimum) return;

            List<Entity> entities = new List<Entity>(world.Entities);
            foreach (Entity entity in entities)
            {
                if (apples >= appleMinimum) return;
                if (!entity.IsAlive) continue;
                if (!entity.Species.Name.Equals("tree", StringComparison.InvariantCultureIgnoreCase)) continue;
                if (random.NextDouble() > FruitChance) continue;

                Entity fruit = apple.Factory(world);
                fruit.Position = entity.Position + new Vector(
                    (float)random.NextDouble() * 4.0f - 2.0f,
                    (float)random.NextDouble() * 4.0f - 2.0f);
                world.Entities.Add(fruit);
                ++apples;
            }
        }
        #endregion
    }
}
