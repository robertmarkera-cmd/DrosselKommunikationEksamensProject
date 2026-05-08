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
            // Set the QuestPDF license to Community to avoid an exception
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            // Arrange
            addQuoteViewModel = new();

            // Setting CurrentQuote's hourly cost
            addQuoteViewModel.CurrentQuote.HourlyCost = 750;

            // Instantiating VariablePriceProducts
            VariablePriceProduct vp1 = new();
            VariablePriceProduct vp2 = new();
            FixedPriceProduct fp1 = new();
            FixedPriceProduct fp2 = new();

            // Instantiating product viewmodels

            List<ProductViewModel> productViewModels = new();
            productViewModels.Add(new ProductViewModel(vp1));
            productViewModels.Add(new ProductViewModel(vp2));
            productViewModels.Add(new ProductViewModel(fp1));
            productViewModels.Add(new ProductViewModel(fp2));

            // then adding products to addQuoteViewModel's selected products
            foreach (ProductViewModel pvm in productViewModels)
            {
                // Setting IsSelected to true
                pvm.IsSelected = true;

                // Setting HoursUsed to 1 (if product is a VariablePriceProduct)
                if (pvm.Product is VariablePriceProduct)
                {
                    pvm.TimeSpentModel.HoursUsed = 1;
                }
                // Setting Price to 1000 (if product is a FixedPriceProduct)
                if (pvm.Product is FixedPriceProduct fp)
                {
                    fp.Price = 1000;
                }
                addQuoteViewModel.SelectedProducts.Add(pvm);
            }

        }

        [TestMethod]
        public void CreateCurrentDraft_WithMixedProducts_CalculatesSubtotalCorrectly()
        {
            // Act
            QuoteDraft draft = addQuoteViewModel.CreateCurrentDraft();
            // Assert
            Assert.AreEqual(3500, draft.Subtotal);
        }
    }
}
