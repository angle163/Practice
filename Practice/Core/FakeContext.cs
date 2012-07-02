#region Using directives

using System;
using Autofac;
using Practice.Pattern;
using Practice.Types;
using Practice.Types.Flag;
using Practice.Types.Interface;

#endregion

namespace Practice.Core
{
    /// <summary>
    /// Context class that accessible with the same instance from all the locations.
    /// </summary>
    public class FakeContext : UserFlag, IDisposable, IHaveServiceLocator
    {
        /// <summary>
        /// The _context lifetime container.
        /// </summary>
        protected ILifetimeScope _contextLifetimeContainer;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FakeContext" /> class. 
        ///   ContextFake Constructor.
        /// </summary>
        public FakeContext()
        {
            _contextLifetimeContainer = GlobalContainer.Container.BeginLifetimeScope(FakeLifetimeScope.Context);

            // init the respository.
            //_repository = new ContextVariableRepository(_variables);

            // init context.
            if (Init != null)
            {
                Init(this, new EventArgs());
            }
        }

        #region Events

        /// <summary>
        /// On ContextFake Constructor Call.
        /// </summary>
        public event EventHandler<EventArgs> Init;

        /// <summary>
        /// On ContextFake Unload Call.
        /// </summary>
        public event EventHandler<EventArgs> Unload;

        #endregion

        #region Properties

        public static FakeContext Current
        {
            get { return PageSingleton<FakeContext>.Instance; }
        }

        public IServiceLocator ServiceLocator
        {
            get { return _contextLifetimeContainer.Resolve<IServiceLocator>(); }
        }

        #endregion

        #region ServiceLocatorExtension

        #region IDisposable

        public void Dispose()
        {
            if (Unload != null)
            {
                Unload(this, new EventArgs());
            }

            _contextLifetimeContainer.Dispose();
        }

        #endregion

        #endregion
    }
}