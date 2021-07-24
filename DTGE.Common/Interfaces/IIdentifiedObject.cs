using System;
using System.Collections.Generic;
using System.Text;

namespace DTGE.Common.Interfaces
{
    public interface IIdentifiedObject : IEquatable<IIdentifiedObject>, IGameSerializable
    {
        /// <summary>
        ///  A unique ID for the Object.
        /// </summary>
        Guid Id { get; set; }
        ISet<string> Tags { get; set; }
    }
}
