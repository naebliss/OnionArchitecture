using Autofac;
using Onion.ApplicationServices.CombatServices;
using Onion.ApplicationServices.DungeonServices;
using Onion.ApplicationServices.GameServices;
using Onion.Core;
using Onion.DomainServices;
using Onion.Infrastructure;
using Onion.Infrastructure.Fake;
using System;

namespace Onion.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Onion quest!");

            var container = GetContainer();

            var hero = new Hero()
            {
                Name = "King Arthur",
                HitPoints = 200,
                Armor = 5,
                Damage = 5
            };

            //var hero = new Hero()
            //{
            //    Name = "Brave sir robin",
            //    HitPoints = 2,
            //    Armor = 0,
            //    Damage = 1
            //};

            var dungeonOptions = new DungeonCreationOptions()
            {
                MonsterDifficulty = Difficulty.Medium,
                EndBossDifficulty = Difficulty.Hard,
                NrOfMonsters = 50
            };

            var gameEngine = container.Resolve<IGameEngine>();

            var dungeon = gameEngine.NewGame(hero, dungeonOptions);

            var questLog = gameEngine.Play();

            Console.Write(questLog.ToString());

            Console.ReadLine();
        }

        private static Autofac.IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GameEngine>().As<IGameEngine>();
            builder.RegisterType<DungeonFactory>().As<IDungeonFactory>();
            builder.RegisterType<CombatEngine>().As<ICombatEngine>();

            builder.RegisterType<Repository<Dungeon>>().As<IRepository<Dungeon>>();

            //builder.RegisterType<FakeRepository<Dungeon>>().As<IRepository<Dungeon>>();

            return builder.Build();
        }

    }
}
