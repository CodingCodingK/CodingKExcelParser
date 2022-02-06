using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace CodingK_WpfMvvmBase
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose() => Disposables.Dispose();

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual void InitData()
        {

        }
        protected virtual void RegisterCommand()
        {

        }
        public ViewModelBase()
        {
            InitData();
            RegisterCommand();
        }
    }
}
