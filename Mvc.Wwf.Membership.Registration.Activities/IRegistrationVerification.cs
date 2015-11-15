﻿namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System;

    public interface IRegistrationVerification<T>
        where T : class
    {
        #region Public Methods and Operators

        Guid VerifyRegistration(RegistrationData data);

        #endregion

    }
}