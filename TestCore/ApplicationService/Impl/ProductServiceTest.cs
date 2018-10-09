using System;
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
        public void CreateProductTest_CallDataSource()
        {
            var dataSource = new Mock<IProductRepository>();
            var product = new Product()
            {
                Id = 1,
                Name = "name",
                Desc = "desc"
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
                Name = "name",
                Desc = "desc"
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
        
    }
}