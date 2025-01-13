using System;
using System.Runtime.CompilerServices;
using ReactiveLibraryFacade.DataStructures;
using CompositeDisposable = ReactiveLibraryFacade.DataStructures.CompositeDisposable;
using DisposableBag = ReactiveLibraryFacade.DataStructures.DisposableBag;

namespace ReactiveLibraryFacade.Extensions
{
    public static class DisposablesRegisterExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable ToDisposable<TKey>(this DisposablesRegister<TKey> register)
        {
            var disposables = register.Values;

            return IDisposableExtension.Combine(disposables);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposableBag ToDisposableBag<TKey>(this DisposablesRegister<TKey> register)
        {
            var disposables = register.Values;

            return new DisposableBag(disposables);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CompositeDisposable ToDisposableComposite<TKey>(this DisposablesRegister<TKey> register)
        {
            var disposables = register.Values;

            return new CompositeDisposable(disposables);
        }
    }
}