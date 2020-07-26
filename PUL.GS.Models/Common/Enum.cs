using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models.Common
{
    public enum HttpVerb : ushort
    {
        /// <summary>
        /// Indefinido
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Consumo GET
        /// </summary>
        Get = 1,
        /// <summary>
        /// Consumo POST
        /// </summary>
        Post = 2,
        /// <summary>
        /// Consumo PUT
        /// </summary>
        Put = 3,
        /// <summary>
        /// Consumo DELETE
        /// </summary>
        Delete = 4
    }

    public enum Status
    {
        Enabled = 1,
        Disabled = 0
    }
}
