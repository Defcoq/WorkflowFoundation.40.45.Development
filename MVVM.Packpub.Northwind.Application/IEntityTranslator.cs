using MVVM.Packpub.Northwind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.Application
{
    public interface IEntityTranslator<M, D> where M : ModelBase
    {
        M CreateModel(D dto);
        M UpdateModel(M model, D dto);
        D CreateDto(M model);
        D UpdateDto(D dto, M model);
    }
}
