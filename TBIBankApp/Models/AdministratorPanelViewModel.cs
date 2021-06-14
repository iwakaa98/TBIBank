using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIBankApp.Models
{
    public class AdministratorPanelViewModel
    {
        public ICollection<UserDTO> Users { get; set; }
    }
}
