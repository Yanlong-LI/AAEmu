using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Managers.UnitManagers;
using AAEmu.Game.Core.Managers.World;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Network.Login;
using AAEmu.Game.Core.Network.Stream;
using AAEmu.Game.Utils.Scripts;
using Microsoft.Extensions.Hosting;
using NLog;

namespace AAEmu.Game
{
    public class GameService : IHostedService, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.Info("Starting daemon: AAEmu.Game");

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            TaskIdManager.Instance.Initialize();
            TaskManager.Instance.Initialize();

            ObjectIdManager.Instance.Initialize();
            TradeIdManager.Instance.Initialize();

            ItemIdManager.Instance.Initialize();
            CharacterIdManager.Instance.Initialize();
            FamilyIdManager.Instance.Initialize();
            ExpeditionIdManager.Instance.Initialize();
            VisitedSubZoneIdManager.Instance.Initialize();
            PrivateBookIdManager.Instance.Initialize();
            FriendIdManager.Instance.Initialize();
            MateIdManager.Instance.Initialize();
            HousingIdManager.Instance.Initialize();

            ZoneManager.Instance.Load();
            WorldManager.Instance.Load();
            QuestManager.Instance.Load();

            ShipyardManager.Instance.Load();

            FormulaManager.Instance.Load();
            ExpirienceManager.Instance.Load();

            TlIdManager.Instance.Initialize();
            ItemManager.Instance.Load();
            PlotManager.Instance.Load();
            SkillManager.Instance.Load();
            CraftManager.Instance.Load();
            MateManager.Instance.Load();
            SlaveManager.Instance.Load();

            NameManager.Instance.Load();
            FactionManager.Instance.Load();
            ExpeditionManager.Instance.Load();
            CharacterManager.Instance.Load();
            FamilyManager.Instance.Load();
            PortalManager.Instance.Load();
            FriendMananger.Instance.Load();

            NpcManager.Instance.Load();
            DoodadManager.Instance.Load();
            HousingManager.Instance.Load();

            SpawnManager.Instance.Load();
            SpawnManager.Instance.SpawnAll();
            HousingManager.Instance.SpawnAll();
            ScriptCompiler.Compile();

            TimeManager.Instance.Start();
            TaskManager.Instance.Start();
            GameNetwork.Instance.Start();
            StreamNetwork.Instance.Start();
            LoginNetwork.Instance.Start();
            stopWatch.Stop();

            _log.Info("Server started! Took {0}", stopWatch.Elapsed);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.Info("Stopping daemon.");

            SpawnManager.Instance.Stop();
            TaskManager.Instance.Stop();
            GameNetwork.Instance.Stop();
            StreamNetwork.Instance.Stop();
            LoginNetwork.Instance.Stop();

            HousingManager.Instance.Save();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _log.Info("Disposing....");

            LogManager.Flush();
        }
    }
}
