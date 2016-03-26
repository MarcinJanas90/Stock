using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Models;

namespace Stock.Services
{
    public interface IShareValueServiceProvider
    {
        Task GetActualShareValues();
    }
}
