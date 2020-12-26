using System;

namespace DNI.Core.Shared.Attributes
{
    /// <summary>
    /// Scrutor will ignore any classes decorating this attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IgnoreScanningAttribute : Attribute
    {
        public IgnoreScanningAttribute(bool ignoreScanning = true)
        {
            IgnoreScanning = ignoreScanning;
        }

        public bool IgnoreScanning { get; }
    }
}
