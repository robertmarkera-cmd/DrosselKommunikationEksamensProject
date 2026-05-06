using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Services.Interfaces
{
    public enum ProductKind
    {
        FixedPrice,
        VariablePrice
    }

    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        ProductKind Kind { get; }
        double ProductPrice { get; }
    }
}
