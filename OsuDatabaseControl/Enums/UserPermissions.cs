using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuDatabaseControl.Enums
{
    [Flags]
    public enum UserPermissions
    {
        None = 0,
        Normal = 1,
        Moderator = 2,
        Supporter = 4,
        Friend = 8,
        Peppy = 16,
        WorldCupStaff = 32
    }
}
