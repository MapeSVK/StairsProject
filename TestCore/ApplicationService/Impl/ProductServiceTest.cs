using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using StairsAndShit.Core.ApplicationService.Impl;
using StairsAndShit.Core.DomainService;
using StairsAndShit.Core.Entity;
using Xunit;

namespace TestCore.ApplicationService.Impl
{
    public class ProductServiceTest
    {
        [Fact]
        public void CreateProductTest_ProductWithoutName_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
            };          
            dataSource.Setup(m => m.Create(It.IsAny<Product>())).Returns(product);

            var testedClas = new ProductService(dataSource.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.CreateProduct(product));
            Assert.Equal("You need to specify products name", ex.Message);  
        }
        
        [Fact]
        public void CreateProductTest_ProductWithoutDesc_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name"
            };          
            dataSource.Setup(m => m.Create(It.IsAny<Product>())).Returns(product);

            var testedClas = new ProductService(dataSource.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.CreateProduct(product));
            Assert.Equal("You need to specify products description", ex.Message);  
        }
        
        [Fact]
        public void CreateProductTest_PriceSmallerThan01_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Desc = "desc",
                              
            };
            dataSource.Setup(m => m.Create(It.IsAny<Product>())).Returns(product);

            var testedClas = new ProductService(dataSource.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.CreateProduct(product));
            Assert.Equal("Price cannot be smaller than 0.1", ex.Message);  
        }
               
        [Fact]
        public void CreateProductTest_CallDataSource()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Desc = "desc",
                Price = 5,
                Type = 'n',
	            ImageLink = "asdas"
                
            };          
           dataSource.Setup(m => m.Create(It.IsAny<Product>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);

            testedClas.CreateProduct(product);
            
            dataSource.Verify(m => m.Create(It.IsAny<Product>()), Times.Once); 
        }
        
        [Fact]
        public void GetProductByIdTest_IdSmallerThan1_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = -1,
            };          
            dataSource.Setup(m => m.GetProductById(It.IsAny<int>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.GetProductById(product.Id));
            Assert.Equal("Id cannot be smaller than 1", ex.Message);  
           
        }
        
        [Fact]
        public void GetProductByIdTest_CallDataSource()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
            };          
            dataSource.Setup(m => m.GetProductById(It.IsAny<int>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);
 
            testedClas.GetProductById(product.Id);
            
            dataSource.Verify(m => m.GetProductById(It.IsAny<int>()), Times.Once); 
        }
        
        [Fact]
        public void UpdateProductTest_CallDataSource()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Desc = "desc",
                Price = 5,
	            ImageLink = "url"
                
            };          
            dataSource.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);

            testedClas.UpdateProduct(product);
            
            dataSource.Verify(m => m.UpdateProduct(It.IsAny<Product>()), Times.Once); 
        }
          
        [Fact]
        public void UpdateProductTest_ProductWithoutName_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
            };          
            dataSource.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(product);

            var testedClas = new ProductService(dataSource.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.UpdateProduct(product));
            Assert.Equal("You need to specify products name", ex.Message);  
        }
        
        [Fact]
        public void UpdateProductTest_ProductWithoutDesc_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name"
	            
            };          
            dataSource.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(product);

            var testedClas = new ProductService(dataSource.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.UpdateProduct(product));
            Assert.Equal("You need to specify products description", ex.Message);  
        }
        
        [Fact]
        public void UpdateProductTest_PriceSmallerThan01_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Desc = "desc",
                              
            };
            dataSource.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(product);

            var testedClas = new ProductService(dataSource.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.UpdateProduct(product));
            Assert.Equal("Price cannot be smaller than 0.1", ex.Message);  
        }
        
        [Fact]
        public void UpdateProductTest_IdSmallerThan1_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = -1,
            };          
            dataSource.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.UpdateProduct(product));
            Assert.Equal("Id cannot be smaller than 1", ex.Message);            
        }
        
        [Fact]
        public void DeleteProductTest_IdSmallerThan1_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = -1,
            };          
            dataSource.Setup(m => m.RemoveProduct(It.IsAny<int>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.DeleteProduct(product.Id));
            Assert.Equal("Id cannot be smaller than 1", ex.Message);  
        }
        
        [Fact]
        public void DeleteProductTest_CallDataSource()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
            };          
            dataSource.Setup(m => m.RemoveProduct(It.IsAny<int>())).Returns(product);
            
            var testedClas = new ProductService(dataSource.Object);
 
            testedClas.DeleteProduct(product.Id);
            
            dataSource.Verify(m => m.RemoveProduct(It.IsAny<int>()), Times.Once); 
        }
        
        [Fact]
        public void ReadAllProducts_CurrentPageSmallerThan_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var filter = new Filter()
            {
               CurrentPage = -1,
                ItemsPrPage = 5
            };          
            dataSource.Setup(m => m.ReadAllProducts(It.IsAny<Filter>()));
            
            var testedClas = new ProductService(dataSource.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.ReadAllProducts(filter));
            Assert.Equal("CurrentPage and ItemsPage Must zero or more", ex.Message);     
        }
        
        [Fact]
        public void ReadAllProducts_ItemPrPageSmallerThan_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var filter = new Filter()
            {
                CurrentPage = 4,
                ItemsPrPage = -6
            };          
            dataSource.Setup(m => m.ReadAllProducts(It.IsAny<Filter>()));
            
            var testedClas = new ProductService(dataSource.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.ReadAllProducts(filter));
            Assert.Equal("CurrentPage and ItemsPage Must zero or more", ex.Message);     
        }
        
        [Fact]
        public void ReadAllProducts_CurrentPageTooHigh_InvalidDataException()
        {
            var dataSource = new Mock<IProductRepository>();
            var filter = new Filter()
            {
                CurrentPage = 5,
                ItemsPrPage = 3
            };          
            dataSource.Setup(m => m.ReadAllProducts(It.IsAny<Filter>()));
            
            var testedClas = new ProductService(dataSource.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                testedClas.ReadAllProducts(filter));
            Assert.Equal("Index out bounds, CurrentPage is to high", ex.Message);     
        }
    }
}