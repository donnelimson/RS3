using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum DisconnectionCircumstancesEnums
    {
        [Description("Upon request of the registered consumer based on justifiable reasons")]
        UponRequestOfTheRegisteredConsumerBasedOnJustifiableReasons = 1,

        [Description("When the public safety so requires")]
        WhenThePublicSafetySoRequires = 2,

        [Description("Non-payment of electric bills within the period of time provided in Article 32 of the Magna Carta")]
        NonPaymentOfElectricBillsWithinThePeriodOfTimeProvidedInArticle32OfTheMagnaCarta = 3,

        [Description("Illegal use of electricity under RA 7832 otherwise known as the Anti-Electricity Pilferage Law")]
        IllegalUseOfElectricityUnderRA7832 = 4,

        [Description("Allowing other end-user or persons to be connected to his electrical installation wether for profit or not")]
        AllowingOtherEndUserOrPersonsToBeConnectedToHisElectricalInstallationWetherForProfitOrNot = 5,

        [Description("Upon lawful orders of government agencies and/or occurs")]
        UponLawfulOrdersOfGovernmentAgenciesAndOrOccurs = 6
    }
}
