﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Packpub.Northwind.ViewModel
{
    public interface IOrderDetailsViewModelFactory
    {
        OrderDetailsViewModel CreateInstance( OrderViewModel order);

    }
}
