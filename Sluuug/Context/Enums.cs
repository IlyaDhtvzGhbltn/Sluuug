using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context 
{
    public enum SessionTypes
    {
        Guest ,
        AwaitEmailConfirm,
        Private,
        Exit
    }

    public enum UserStatuses
    {
        AwaitConfirmation,
        Active,
        Blocked,
        Deleted
    }

    public enum ActivationLnkStatus
    {
        Correct,
        Fail
    }
}
