using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Request.Wx;

namespace BLL.Interface
{
    public interface IMiniPaySignService
    {
        public string Sign(MiniPaySignPara para);

        public bool Verify(MiniPayDeSignPara para);
    }
}
