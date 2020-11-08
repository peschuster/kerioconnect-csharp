namespace KerioConnect.Entities
{
    /// <summary>
    /// Email address type.
    /// </summary>
    public enum EmailAddressType
    {
        EmailCustom = 0, ///< no type defined
        EmailWork,
        EmailHome,
        EmailOther,

        // valid for distribution lists only
        RefContact,           ///< Reference to existing conatact
        RefDistributionList   ///< Reference to existing distribution list
    };
}