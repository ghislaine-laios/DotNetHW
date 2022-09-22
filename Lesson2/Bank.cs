using System.Collections.Immutable;

namespace Lesson2
{
    internal interface IUserBank<T> where T : ICash
    {
        public IImmutableList<IATM<T>> ATMs { get; }
    }

    internal interface IBank<T>: IUserBank<T> where T : ICash
    {
        public bool GetAccount(long id, out Account result);

        public void SetAccount(long id, in Account account);
    }

    internal interface ICashBox<Cash> where Cash : ICash
    {
        // Get some cashes from the box.
        public IList<Cash> Get(int number);

        // Add some cashes to the box.
        public void Add(IEnumerable<Cash> cashes);

        public int Count { get; }
    }

    internal class DefaultCashBox : ICashBox<Cash100>
    {
        private Queue<Cash100> cashes = new();

        public IList<Cash100> Get(int number)
        {
            if (number >= cashes.Count) throw new InvalidOperationException("Cash box IS empty.");
            IList<Cash100> result = new List<Cash100>(number);
            for (int i = 0; i < number; ++i)
            {
                result.Add(cashes.Dequeue());
            }
            return result;
        }

        public void Add(IEnumerable<Cash100> cashes)
        {
            foreach (var cash in cashes)
            {
                this.cashes.Enqueue(cash);
            }
        }

        public int Count { get => cashes.Count; }
    }

    internal class DemoCashBox : DefaultCashBox
    {
        public DemoCashBox()
        {
            var cashes = new List<Cash100>();
            for (int i = 0; i < 1000; ++i)
            {
                cashes.Add(new Cash100 { Id = i + 1 });
            }
            Add(cashes);
        }
    }

    internal class DemoBank : IBank<Cash100>
    {
        public DemoBank()
        {
            ATMs = ImmutableArray.Create<IATM<Cash100>>(new ATM[1] {new ATM(this, cashBox.Get(500)) });
            accountDictionary.Add(1000, new Account { Id = 1000, Name = "Demo User 1", Balance = 12000 });
        }

        private DemoCashBox cashBox = new DemoCashBox();
        
        private IDictionary<long, Account> accountDictionary = new Dictionary<long, Account>();

        public IImmutableList<IATM<Cash100>> ATMs { get; }

        public bool GetAccount(long id, out Account result)
        {
            return accountDictionary.TryGetValue(id, out result);
        }

        public void SetAccount(long id, in Account account)
        {
            accountDictionary[id] = account;
        }
    }

}
