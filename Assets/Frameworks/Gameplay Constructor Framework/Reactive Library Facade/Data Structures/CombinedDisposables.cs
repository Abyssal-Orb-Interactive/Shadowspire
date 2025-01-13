using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveLibraryFacade.DataStructures
{
    public sealed class CombinedDisposable : IDisposable
    {
        private readonly IDisposable[] _disposables = null;

        public CombinedDisposable(params IDisposable[] disposables)
        {
            _disposables = disposables;
        }
        
        public CombinedDisposable(IEnumerable<IDisposable> disposables)
        {
            _disposables = disposables.ToArray();
        }

        public void Dispose()
        {
            var lenght = _disposables.Length;
            
            for (var i = 0; i < lenght; i++)
            {
                _disposables[i].Dispose();
            }
        }
    }
    
    public sealed class CombinedDisposable2 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;

        public CombinedDisposable2(IDisposable disposable1, IDisposable disposable2)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
        }
    }
    
    public sealed class CombinedDisposable3 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;
        private readonly IDisposable _disposable3 = null;

        public CombinedDisposable3(IDisposable disposable1, IDisposable disposable2, IDisposable disposable3)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
            _disposable3 = disposable3;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
            _disposable3.Dispose();
        }
    }
    
    public sealed class CombinedDisposable4 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;
        private readonly IDisposable _disposable3 = null;
        private readonly IDisposable _disposable4 = null;

        public CombinedDisposable4(IDisposable disposable1, IDisposable disposable2, IDisposable disposable3, IDisposable disposable4)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
            _disposable3 = disposable3;
            _disposable4 = disposable4;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
            _disposable3.Dispose();
            _disposable4.Dispose();
        }
    }
    
    public sealed class CombinedDisposable5 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;
        private readonly IDisposable _disposable3 = null;
        private readonly IDisposable _disposable4 = null;
        private readonly IDisposable _disposable5 = null;

        public CombinedDisposable5(IDisposable disposable1, IDisposable disposable2, IDisposable disposable3, IDisposable disposable4, IDisposable disposable5)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
            _disposable3 = disposable3;
            _disposable4 = disposable4;
            _disposable5 = disposable5;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
            _disposable3.Dispose();
            _disposable4.Dispose();
            _disposable5.Dispose();
        }
    }
    
    public sealed class CombinedDisposable6 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;
        private readonly IDisposable _disposable3 = null;
        private readonly IDisposable _disposable4 = null;
        private readonly IDisposable _disposable5 = null;
        private readonly IDisposable _disposable6 = null;

        public CombinedDisposable6(IDisposable disposable1, IDisposable disposable2, IDisposable disposable3, IDisposable disposable4, IDisposable disposable5, IDisposable disposable6)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
            _disposable3 = disposable3;
            _disposable4 = disposable4;
            _disposable5 = disposable5;
            _disposable6 = disposable6;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
            _disposable3.Dispose();
            _disposable4.Dispose();
            _disposable5.Dispose();
            _disposable6.Dispose();
        }
    }
    
    public sealed class CombinedDisposable7 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;
        private readonly IDisposable _disposable3 = null;
        private readonly IDisposable _disposable4 = null;
        private readonly IDisposable _disposable5 = null;
        private readonly IDisposable _disposable6 = null;
        private readonly IDisposable _disposable7 = null;

        public CombinedDisposable7(IDisposable disposable1, IDisposable disposable2, IDisposable disposable3, IDisposable disposable4, IDisposable disposable5, IDisposable disposable6, IDisposable disposable7)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
            _disposable3 = disposable3;
            _disposable4 = disposable4;
            _disposable5 = disposable5;
            _disposable6 = disposable6;
            _disposable7 = disposable7;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
            _disposable3.Dispose();
            _disposable4.Dispose();
            _disposable5.Dispose();
            _disposable6.Dispose();
            _disposable7.Dispose();
        }
    }
    
    public sealed class CombinedDisposable8 : IDisposable
    {
        private readonly IDisposable _disposable1 = null; 
        private readonly IDisposable _disposable2 = null;
        private readonly IDisposable _disposable3 = null;
        private readonly IDisposable _disposable4 = null;
        private readonly IDisposable _disposable5 = null;
        private readonly IDisposable _disposable6 = null;
        private readonly IDisposable _disposable7 = null;
        private readonly IDisposable _disposable8 = null;

        public CombinedDisposable8(IDisposable disposable1, IDisposable disposable2, IDisposable disposable3, IDisposable disposable4, IDisposable disposable5, IDisposable disposable6, IDisposable disposable7, IDisposable disposable8)
        {
            _disposable1 = disposable1;
            _disposable2 = disposable2;
            _disposable3 = disposable3;
            _disposable4 = disposable4;
            _disposable5 = disposable5;
            _disposable6 = disposable6;
            _disposable7 = disposable7;
            _disposable8 = disposable8;
        }

        public void Dispose()
        {
            _disposable1.Dispose();
            _disposable2.Dispose();
            _disposable3.Dispose();
            _disposable4.Dispose();
            _disposable5.Dispose();
            _disposable6.Dispose();
            _disposable7.Dispose();
            _disposable8.Dispose();
        }
    }
}