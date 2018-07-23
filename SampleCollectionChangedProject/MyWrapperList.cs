using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleCollectionChangedProject
{
    public class MyWrapperList<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public MyWrapperList()
            : this(new List<T>()) { }
        
        public MyWrapperList(IList<T> source)
        {
            Source = source;
            if (source is INotifyCollectionChanged collectionChangedSource)
            {
                collectionChangedSource.CollectionChanged += _SourceCollectionChanged;
                _isSourceCollectionChanged = true;
            }

            if (source is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
            }
        }

        readonly bool _isSourceCollectionChanged;
        
        public IList<T> Source { get; }
        
        public T this[int index]
        {
            get => Source[index];
            set => Source[index] = value;
        }
        
        public int Count => Source.Count;
        
        public bool IsReadOnly => Source.IsReadOnly;
        
        public void Add(T item)
        {
            Source.Add(item);
            if (!_isSourceCollectionChanged)
            {
                CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
            }
        }
        
        public void Clear()
        {
            Source.Clear();
            if (!_isSourceCollectionChanged)
            {
                CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
            }
        }

        public void Insert(int index, T item)
        {
            Source.Insert(index, item);
            if (!_isSourceCollectionChanged)
            {
                CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
            }
        }

        public bool Remove(T item)
        {
            if (!_isSourceCollectionChanged)
            {
                CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }

            var result = Source.Remove(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            return result;
        }

        public void RemoveAt(int index)
        {
            if (!_isSourceCollectionChanged)
            {
                CollectionChanged?.Invoke(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source[index]));
            }

            Source.RemoveAt(index);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
        }

        public bool Contains(T item) { return Source.Contains(item); }

        public void CopyTo(T[] array, int arrayIndex) { Source.CopyTo(array, arrayIndex); }
        
        public int IndexOf(T item) { return Source.IndexOf(item); }
        
        public IEnumerator<T> GetEnumerator() { return Source.GetEnumerator(); }
        
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        void _SourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
