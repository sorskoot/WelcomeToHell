using System;
using System.Collections.Generic;
using Sorskoot.Ioc;
using UniRx;
using Random = UnityEngine.Random;

namespace WelcomeToHell
{

    public interface IGameState
    {
        IReadOnlyReactiveProperty<bool> ShowHelp { get; }
        IReadOnlyReactiveProperty<bool> IsPrinting { get; }
        IReadOnlyReactiveProperty<int> PrintsInQueue { get; }
        IReadOnlyReactiveProperty<bool> ContractOnTable { get; }
        IReadOnlyReactiveProperty<Sins?> CurrentSin { get; }
        IReadOnlyReactiveProperty<int> Score { get; }
        IReadOnlyReactiveProperty<int> Failures { get; }

        void AddPrintToQueue();
        void PlaceContractOnTable();
        void TakeContractFromTable();
        void Stamp(string stampName);
        void Start();
        void ContractPlaced(string stampName);
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
        public IReadOnlyReactiveProperty<int> PrintsInQueue => printsInQueue;
        
        private readonly BoolReactiveProperty contractOnTable = new BoolReactiveProperty(false);
        public IReadOnlyReactiveProperty<bool> ContractOnTable => contractOnTable;
        
        private readonly ReactiveProperty<Sins?> currentSin = new ReactiveProperty<Sins?>(null);
        public IReadOnlyReactiveProperty<Sins?> CurrentSin => currentSin;
        
        private readonly IntReactiveProperty score = new IntReactiveProperty(0);
        public IReadOnlyReactiveProperty<int> Score => score;
        
        private readonly IntReactiveProperty failures = new IntReactiveProperty(0);
        public IReadOnlyReactiveProperty<int> Failures => failures;


        private readonly ILogger logger;
        
        

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
                // printing for 8 seconds...
                Scheduler.MainThread.Schedule(8000f, PrintDone);
            };
        }

        private void PrintDone()
        {
            // 1s cooldown... 
            Scheduler.MainThread.Schedule(1000f, () =>
            {
                isPrinting.Value = false;
                MessageBroker.Default.Publish(new PrintDoneMessage());
                printsInQueue.Value--;

                if (printsInQueue.Value > 0)
                {
                    StartPrinting();
                }
            });
        }

        public void PlaceContractOnTable()
        {
            contractOnTable.Value = true;
        }

        public void TakeContractFromTable()
        {
            contractOnTable.Value = false;
        }

        private void NewDeceased()
        {
            currentSin.SetValueAndForceNotify((Sins)Random.Range(0, 7));
        }
        
        public void Stamp(string stampName)
        {
            // Only place stamp when contract is on the table
            if (contractOnTable.Value)
            {
                MessageBroker.Default.Publish(new StampMessage(stampName));
            }
        }

        public void Start()
        {
            failures.Value = 0;
            score.Value = 0;
            Scheduler.MainThread.Schedule(2000, NewDeceased);
        }

        public void ContractPlaced(string stampName)
        {
            if(Enum.TryParse(stampName.Split(' ')[1], out Sins sin))
            {
                MessageBroker.Default.Publish(new ContractPlacedMessage());
                Scheduler.MainThread.Schedule(2000, NewDeceased);
                
                if (sin == currentSin.Value)
                {
                    // Did it right
                    score.Value++;
                }
                else
                {
                    failures.Value++;
                    if (failures.Value == 3)
                    {
                        // You're fired!
                        // start over
                        Scheduler.MainThread.Schedule(5000, Start);
                    }                    
                }
            }
        }
    }
}