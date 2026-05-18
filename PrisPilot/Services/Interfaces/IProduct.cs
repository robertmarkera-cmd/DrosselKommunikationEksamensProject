using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Services.Interfaces
{
    public interface IProduct
    {
        int ProductID { get; }
        string Name { get; }
        string Description { get; }
        double ProductPrice { get; }
    }
}
