using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMA.Framework
{
    /// <summary>
    ///     Exception métier avec un message affichable à l'utilisateur
    /// </summary>
    public interface IBusinessException
    {
        /// <summary>
        ///     Message affichable à l'utilisateur
        /// </summary>
        /// <value>
        ///     Message affichable à l'utilisateur
        /// </value>
        string UserMessage { get; set; }
    }
}