using Services.INtefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class BaseService
    {
        public IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
    }
}
