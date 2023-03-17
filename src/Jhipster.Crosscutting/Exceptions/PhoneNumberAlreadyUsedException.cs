using Jhipster.Crosscutting.Constants;

namespace Jhipster.Crosscutting.Exceptions
{
    public class PhoneNumberAlreadyUsedException : BadRequestAlertException
    {
        public PhoneNumberAlreadyUsedException() : base(ErrorConstants.PhoneNumberAlreadyUsedType, "PhoneNumber is already in use!",
            "userManagement", "phonenumberexists")
        {
        }
    }
}
