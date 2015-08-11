using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServices
{
    interface IUDTDataProvider
    {
        UDTProvider.UDTProvider Udt { get; set; }
        bool InitializeConnection();
        void InitializeUDT(string UdtName);
    }
}
