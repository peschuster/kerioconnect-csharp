namespace KerioConnect.Entities
{
    /// <summary>
    /// Type of a contact phone number
    /// </summary>
    public enum PhoneNumberType {
    
        TypeCustom = 0, ///< no type defined
        TypeAssistant,
        TypeWorkVoice,
        TypeWorkFax,
        TypeCallback,
        TypeCar,
        TypeCompany,
        TypeHomeVoice,
        TypeHomeFax,
        TypeIsdn,
        TypeMobile,
        TypeOtherVoice,
        TypeOtherFax,
        TypePager,
        TypePrimary,
        TypeRadio,
        TypeTelex,
        TypeTtyTdd
    };
}