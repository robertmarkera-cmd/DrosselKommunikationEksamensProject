using PrisPilot;
using PrisPilot.Models;
using PrisPilot.ViewModels;

namespace PPTests
{
    [TestClass]
    public sealed class Test1
    {
        AddQuoteViewModel addQuoteViewModel;

        [TestInitialize]
        public void Init()
        {
            // Instantiating VariablePriceProducts
            VariablePriceProduct vp1 = new();
            VariablePriceProduct fp1 = new();

            // Initializing
            

            // Arrange
            addQuoteViewModel = new();
            //addQuoteViewModel.SelectedProducts.Add();
        }

        [TestMethod]
        public void CreateCurrentDraft_WithMixedProducts_CalculatesSubtotalCorrectly()
        {
        }
    }
}
