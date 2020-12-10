using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Enumerations
{
    /// <summary>
    /// The encryption classification used for encrypting data
    /// </summary>
    public enum EncryptionClassification
    {
        /// <summary>
        /// Level 3: Requires encryption to protect unique data belonging to an entity, such as an e-mail address or national identity details
        /// </summary>
        PersonalData = 3,
        /// <summary>
        /// Level 2: Requires encryption to protect common data that can be shared between entities, but when combined with other common fields can lead to personal identification of an entity
        /// </summary>
        CommonData = 2,
        /// <summary>
        /// Level 1: Requires encryption to protect shared data that would not lead to direct identification to an entity, but should be protected securely
        /// </summary>
        SharedData = 1
    }
}
