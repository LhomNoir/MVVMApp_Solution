using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMA.Framework
{
    /// <summary>
    ///     Classe initialisable
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        ///     Instance initialisée
        /// </summary>
        /// <value>
        ///     Instance initialisée
        /// </value>
        bool IsInitialized { get; }

        /// <summary>
        ///     Initialise l'instance
        /// </summary>
        void Initialize();
    }
}