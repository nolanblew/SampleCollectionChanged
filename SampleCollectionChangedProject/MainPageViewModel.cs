using System.Windows.Input;

namespace SampleCollectionChangedProject
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            AddCommand = new DelegateCommand(_AddItem);

            for (int i = 0; i < 5; i++)
            {
                _AddItem();
            }
        }

        private int _itemCount;

        public ICommand AddCommand { get; }

        public MyWrapperList<string> MyList { get; } = new MyWrapperList<string>();

        void _AddItem()
        {
            MyList.Add($"Item # {++_itemCount}");
        }
    }
}
