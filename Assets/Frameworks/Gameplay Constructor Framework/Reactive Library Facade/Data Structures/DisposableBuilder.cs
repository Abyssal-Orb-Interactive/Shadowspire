﻿#nullable enable
using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace ReactiveLibraryFacade.DataStructures
{
    public ref struct DisposableBuilder
    {
        private IDisposable? _disposable1;
        private IDisposable? _disposable2;
        private IDisposable? _disposable3;
        private IDisposable? _disposable4;
        private IDisposable? _disposable5;
        private IDisposable? _disposable6;
        private IDisposable? _disposable7;
        private IDisposable? _disposable8;
        private IDisposable[]? _disposables;

        private int _count;
        
        public void Add(in IDisposable disposable)
        {
            switch (_count)
            {
                case -1:
                    break;
                case 0:
                    _disposable1 = disposable;
                    break;
                case 1:
                    _disposable2 = disposable;
                    break;
                case 2:
                    _disposable3 = disposable;
                    break;
                case 3:
                    _disposable4 = disposable;
                    break;
                case 4:
                    _disposable5 = disposable;
                    break;
                case 5:
                    _disposable6 = disposable;
                    break;
                case 6:
                    _disposable7 = disposable;
                    break;
                case 7:
                    _disposable8 = disposable;
                    break;
                default:
                    AddToArray(in disposable);
                    break;
            }
            _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddToArray(in IDisposable disposable)
        {
            if (_count == 8)
            {
                var newDisposables = ArrayPool<IDisposable>.Shared.Rent(16);
                newDisposables[8] = disposable;
                newDisposables[0] = _disposable1!;
                newDisposables[1] = _disposable2!;
                newDisposables[2] = _disposable3!;
                newDisposables[3] = _disposable4!;
                newDisposables[4] = _disposable5!;
                newDisposables[5] = _disposable6!;
                newDisposables[6] = _disposable7!;
                newDisposables[7] = _disposable8!;
                
                _disposable1 = _disposable2 = _disposable3 = _disposable4 = _disposable5 = _disposable6 = _disposable7 = _disposable8 = null;
                
                _disposables = newDisposables; 
            }
            else
            {
                if (_disposables!.Length == _count)
                {
                    var newDisposables = ArrayPool<IDisposable>.Shared.Rent(_count * 2);
                    Array.Copy(_disposables, newDisposables, _disposables.Length);
                    ArrayPool<IDisposable>.Shared.Return(_disposables, clearArray: true);
                    _disposables = newDisposables;
                }
                _disposables[_count] = disposable;
            }
        }
        
        public IDisposable Build()
        {
            IDisposable result = new EmptyDisposable();
            switch (_count)
            {
                case -1: 
                    break;
                case 0:
                    break;
                case 1:
                    result = _disposable1!;
                    break;
                case 2:
                    result = new CombinedDisposable2(
                        _disposable1!,
                        _disposable2!
                    );
                    break;
                case 3:
                    result = new CombinedDisposable3(
                        _disposable1!,
                        _disposable2!,
                        _disposable3!
                    );
                    break;
                case 4:
                    result = new CombinedDisposable4(
                        _disposable1!,
                        _disposable2!,
                        _disposable3!,
                        _disposable4!
                    );
                    break;
                case 5:
                    result = new CombinedDisposable5(
                        _disposable1!,
                        _disposable2!,
                        _disposable3!,
                        _disposable4!,
                        _disposable5!
                    );
                    break;
                case 6:
                    result = new CombinedDisposable6(
                        _disposable1!,
                        _disposable2!,
                        _disposable3!,
                        _disposable4!,
                        _disposable5!,
                        _disposable6!
                    );
                    break;
                case 7:
                    result = new CombinedDisposable7(
                        _disposable1!,
                        _disposable2!,
                        _disposable3!,
                        _disposable4!,
                        _disposable5!,
                        _disposable6!,
                        _disposable7!
                    );
                    break;
                case 8:
                    result = new CombinedDisposable8(
                        _disposable1!,
                        _disposable2!,
                        _disposable3!,
                        _disposable4!,
                        _disposable5!,
                        _disposable6!,
                        _disposable7!,
                        _disposable8!
                    );
                    break;
                default:
                    result = new CombinedDisposable(_disposables!.AsSpan(0, _count).ToArray());
                    break;
            }

            Dispose();
            return result;
        }
        
        public void Dispose()
        {
            if (_count == -1) return;
            
            _disposable1 = _disposable2 = _disposable3 = _disposable4 = _disposable5 = _disposable6 = _disposable7 = _disposable8 = null;
            if (_disposables != null)
            {
                ArrayPool<IDisposable>.Shared.Return(_disposables, clearArray: true);
            }
            _count = -1;
        }
    }
}