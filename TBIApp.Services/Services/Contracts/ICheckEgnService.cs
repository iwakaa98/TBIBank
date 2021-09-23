using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TBIApp.Services.Services.Contracts
{
    public interface ICheckEgnService
    {
        bool IsValidEgn(string egn);
        bool isValidMonthAndDate(int[] egnArray);
        int EGNCount(int[] egnArray);
    }
}
