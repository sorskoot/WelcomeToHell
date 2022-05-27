using System;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UniRx;

namespace WelcomeToHell
{

    public interface IGameState
    {
        IReadOnlyReactiveProperty<bool> ShowHelp { get; }
        IReadOnlyReactiveProperty<bool> IsPrinting { get; }
        IReadOnlyReactiveProperty<int> PrintsInQueue { get; }

        void AddPrintToQueue();
    }

    public class GameState:IGameState
    {
        public GameState(Sorskoot.Ioc.ILogger logger)
        {
            this.logger = logger;
        }
        
        private readonly BoolReactiveProperty showHelp = new BoolReactiveProperty();
        public IReadOnlyReactiveProperty<bool> ShowHelp => showHelp;

        private readonly BoolReactiveProperty isPrinting = new BoolReactiveProperty();
        public IReadOnlyReactiveProperty<bool> IsPrinting => isPrinting;
        
        private readonly IntReactiveProperty printsInQueue = new IntReactiveProperty(0);
        private readonly ILogger logger;
        
        public IReadOnlyReactiveProperty<int> PrintsInQueue { get; }
        
        public void AddPrintToQueue()
        {
            printsInQueue.Value++;
            logger.Log($"Added a print to the queue");
            
            StartPrinting();
        }

        private void StartPrinting()
        {
            if (!isPrinting.Value)
            {
                isPrinting.Value = true;
                Scheduler.MainThread.Schedule(5000f, PrintDone);
            };
        }

        private void PrintDone()
        {
            isPrinting.Value = false;
            MessageBroker.Default.Publish(new PrintDoneMessage());
            printsInQueue.Value--;

            if (printsInQueue.Value > 0)
            {
                StartPrinting();
            }
        }
    }
}