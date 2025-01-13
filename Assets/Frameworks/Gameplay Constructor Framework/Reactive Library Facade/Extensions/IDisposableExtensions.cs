using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ReactiveLibraryFacade.DataStructures;

namespace ReactiveLibraryFacade.Extensions
{
    public static class IDisposableExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Create(in Action onDispose)
        {
            return new Disposable(onDispose);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposableBuilder CreateBuilder()
        {
            return new DisposableBuilder();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DisposableBuilder CreateBuilder<T>(this T disposable) where T : IDisposable
        {
            var builder = CreateBuilder();
            disposable.AddTo(builder);
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddTo<T>(this T disposable, in DisposableBuilder builder) where T : IDisposable
        {
            builder.Add(disposable);
            return disposable;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddTo<T>(this T disposable, in DisposableBag bag) where T : IDisposable
        {
            bag.Add(disposable);
            return disposable;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddTo<T>(this T disposable, in CompositeDisposable composite) where T : IDisposable
        {
            composite.Add(disposable);
            return disposable;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddTo<TKey, T>(this T disposable, in DisposablesRegister<TKey> register, in TKey key) where T : IDisposable
        {
            register[key] = disposable;
            return disposable;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddTo<T>(this T disposable, ICollection<IDisposable> disposables) where T : IDisposable
        {
            disposables.Add(disposable);
            return disposable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2)
        {
            return new CombinedDisposable2(sourceDisposable, disposable2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2, in IDisposable disposable3)
        {
            return new CombinedDisposable3(sourceDisposable, disposable2, disposable3);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2, in IDisposable disposable3, in IDisposable disposable4)
        {
            return new CombinedDisposable4(sourceDisposable, disposable2, disposable3, disposable4);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2, in IDisposable disposable3, in IDisposable disposable4, in IDisposable disposable5)
        {
            return new CombinedDisposable5(sourceDisposable, disposable2, disposable3, disposable4, disposable5);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2, in IDisposable disposable3, in IDisposable disposable4, in IDisposable disposable5, in IDisposable disposable6)
        {
            return new CombinedDisposable6(sourceDisposable, disposable2, disposable3, disposable4, disposable5, disposable6);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2, in IDisposable disposable3, in IDisposable disposable4, in IDisposable disposable5, in IDisposable disposable6, in IDisposable disposable7)
        {
            return new CombinedDisposable7(sourceDisposable, disposable2, disposable3, disposable4, disposable5, disposable6, disposable7);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IDisposable disposable2, in IDisposable disposable3, in IDisposable disposable4, in IDisposable disposable5, in IDisposable disposable6, in IDisposable disposable7, in IDisposable disposable8)
        {
            return new CombinedDisposable8(sourceDisposable, disposable2, disposable3, disposable4, disposable5, disposable6, disposable7, disposable8);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, params IDisposable[] disposables)
        {
            var combinedDisposables  = new IDisposable[disposables.Length + 1];
            combinedDisposables [0] = sourceDisposable;
            Array.Copy(disposables, 0, combinedDisposables, 1, disposables.Length);
            
            return new CombinedDisposable(combinedDisposables);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(params IDisposable[] disposables)
        {
            return new CombinedDisposable(disposables);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(this IDisposable sourceDisposable, in IEnumerable<IDisposable> disposables)
        {
            var combinedDisposables = new List<IDisposable> { sourceDisposable };
            combinedDisposables.AddRange(disposables);

            return new CombinedDisposable(combinedDisposables);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDisposable Combine(in IEnumerable<IDisposable> disposables)
        {
            return new CombinedDisposable(disposables);
        }
    }
}

    