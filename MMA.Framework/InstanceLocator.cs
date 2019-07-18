using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMA.Framework
{
    /// <summary>
    ///     Localisateur d'instances
    /// </summary>
    public class InstanceLocator
    {
        #region Fields

        private static readonly object _locker = new object();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static InstanceLocator _current;

        #endregion

        #region Properties

        /// <summary>
        ///     Instance courante du localisateur d'instances
        /// </summary>
        /// <value>
        ///     Instance courante du localisateur d'instances
        /// </value>
        public static InstanceLocator Current
        {
            get
            {
                lock (_locker)
                {
                    if (_current == null)
                    {
                        _current = new InstanceLocator();
                    }

                    return _current;
                }
            }
        }

        /// <summary>
        ///     Nombre total d'instances créées
        /// </summary>
        /// <value>
        ///     Nombre total d'instances créées
        /// </value>
        public int TotalInstances => Instances.Count;

        private IDictionary<Type, Func<object>> Factories { get; } = new Dictionary<Type, Func<object>>();
        private IDictionary<Type, object> Instances { get; } = new Dictionary<Type, object>();
        private object Locker { get; } = new object();

        #endregion

        #region Methods

        public void ClearInstances()
        {
            Factories.Clear();
            Instances.Clear();
        }

        /// <summary>
        ///     Récupère l'instance d'une interface
        ///     <para>
        ///         Renvoie <c>null</c> si l'interface est introuvable.
        ///     </para>
        /// </summary>
        /// <typeparam name="TInterface">Type de l'interface</typeparam>
        /// <returns>Instance de l'interface</returns>
        public TInterface GetInstance<TInterface>()
        {
            lock (Locker)
            {
                var type = typeof(TInterface);
                if (!Instances.ContainsKey(type))
                {
                    _logger.Warn($"Interface introuvable : [{type}].");
                    return default(TInterface);
                }

                var instance = Instances[type];

                if (instance == null)
                {
                    return CreateInstance<TInterface>();
                }

                return (TInterface)Instances[type];
            }
        }

        /// <summary>
        ///     Enregistre une usine de fabrication d'instance d'une interface par défaut
        ///     <para>
        ///         L'usine sera utilisée lors de la première récupération de l'interface.
        ///     </para>
        /// </summary>
        /// <typeparam name="TInterface">Interface</typeparam>
        /// <typeparam name="TInstance">Instance de l'interface</typeparam>
        public void RegisterFactory<TInterface, TInstance>()
            where TInstance : class, TInterface, new()
        {
            var factory = new Func<TInstance>(() => { return new TInstance(); });
            RegisterFactory<TInterface, TInstance>(factory);
        }

        /// <summary>
        ///     Enregistre une usine de fabrication d'instance d'une interface
        ///     <para>
        ///         L'usine sera utilisée lors de la première récupération de l'interface.
        ///         Si une usine est déjà enregistrée pour cette interface, celle-ci est remplacée.
        ///     </para>
        /// </summary>
        /// <remarks></remarks>
        /// <typeparam name="TInterface">Interface</typeparam>
        /// <typeparam name="TInstance">Instance de l'interface</typeparam>
        /// <param name="factory">Usine de fabrication</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RegisterFactory<TInterface, TInstance>(Func<TInstance> factory)
            where TInstance : class, TInterface, new()
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            lock (Locker)
            {
                var interfaceType = typeof(TInterface);
                if (Instances.ContainsKey(interfaceType))
                {
                    Factories[interfaceType] = factory;
                    _logger.Debug($"Usine de [{interfaceType}] remplacée.");
                }
                else
                {
                    Instances.Add(interfaceType, null);
                    Factories.Add(interfaceType, factory);
                    _logger.Debug($"Usine de [{interfaceType}] enregistrée.");
                }
            }
        }

        /// <summary>
        ///     Enregistre l'instance d'une interface
        /// </summary>
        /// <typeparam name="TInterface">Interface</typeparam>
        /// <param name="instance">Instance de l'interface</param>
        public void RegisterInstance<TInterface>(TInterface instance)
        {
            SaveInstance<TInterface, TInterface>(instance);
        }

        /// <summary>
        ///     Enregistre l'instance d'une interface
        /// </summary>
        /// <typeparam name="TInterface">Interface</typeparam>
        /// <typeparam name="TInstance">Instance de l'interface</typeparam>
        public void RegisterInstance<TInterface, TInstance>()
            where TInstance : class, TInterface, new()
        {
            var instance = new TInstance();
            RegisterInstance<TInterface>(instance);
        }

        private TInterface CreateInstance<TInterface>()
        {
            var type = typeof(TInterface);
            if (!Factories.ContainsKey(type))
            {
                _logger.Warn($"Usine de [{type}] introuvable.");
                return default(TInterface);
            }

            var instance = Factories[type]?.Invoke();
            Instances[type] = instance;
            _logger.Debug($"Instance de [{type}] créée.");
            return (TInterface)instance;
        }

        /// <summary>
        ///     Saves the instance.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <param name="instance">The instance.</param>
        private void SaveInstance<TInterface, TInstance>(TInstance instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            lock (Locker)
            {
                var interfaceType = typeof(TInterface);
                if (Instances.ContainsKey(interfaceType))
                {
                    Instances[interfaceType] = instance;
                    _logger.Debug($"Instance de [{interfaceType}] remplacée.");
                }
                else
                {
                    Instances.Add(interfaceType, instance);
                    _logger.Debug($"Instance de [{interfaceType}] enregistrée.");
                }
            }
        }

        #endregion
    }
}