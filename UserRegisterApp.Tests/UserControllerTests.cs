using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserRegisterApp.Controllers;
using UserRegisterApp.Data;
using UserRegisterApp.Models; 


public class UserControllerTests // ...
{
    // DB Context'i testten önce hazırlayıp zorunlu alanları dolduran yardımcı metot
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);

        // Zorunlu alanlar doldurularak SaveChanges hatası önlenir
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Id = 1, Name = "Test User 1", Email = "test1@mail.com", PasswordHash = "HP1", ProfileImagePath = "/images/1.png" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@mail.com", PasswordHash = "HP2", ProfileImagePath = "/images/2.png" }
            );
            context.SaveChanges();
        }
        return context;
    }

    // Sahte IWebHostEnvironment'ı oluşturan yardımcı metot
    private IWebHostEnvironment CreateMockWebHostEnvironment()
    {
        var mockEnv = new Mock<IWebHostEnvironment>();
        mockEnv.Setup(e => e.WebRootPath).Returns("/mock-wwwroot");
        return mockEnv.Object;
    }

    [Fact]
    public void Index_ReturnsViewWithUserList()
    {
    
        using var context = CreateInMemoryDbContext(); 
        var controller = new UserController(context, CreateMockWebHostEnvironment());

        
        var result = controller.Index();

      
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<User>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count); 
    }

    
    //  Create
   
    [Fact]
    public async Task Create_ValidModel_AddsUserAndRedirectsToIndex()
    {
        
        using var context = CreateInMemoryDbContext();
        var controller = new UserController(context, CreateMockWebHostEnvironment());

      
        var tempData = new TempDataDictionary(new Microsoft.AspNetCore.Http.DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        controller.TempData = tempData;

        
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.Length).Returns(100);
        mockFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var model = new UserViewModel
        {
            Name = "New User",
            Email = "new@user.com",
            Password = "password123",
            ProfileImage = mockFile.Object
        };


        var result = await controller.Create(model);

       
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);

        
        var userInDb = context.Users.Last();
        Assert.NotNull(userInDb);
        Assert.True(BCrypt.Net.BCrypt.Verify("password123", userInDb.PasswordHash));
    }

    //Silme
    
    public async Task DeleteConfirmed_ExistingId_RemovesUserAndRedirectsToIndex()
    {
        
        using var context = CreateInMemoryDbContext();
        var controller = new UserController(context, CreateMockWebHostEnvironment());

        var tempData = new TempDataDictionary(new Microsoft.AspNetCore.Http.DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        controller.TempData = tempData;

        var initialCount = context.Users.Count(); 
        int userIdToDelete = 1;

      
        var result = await controller.DeleteConfirmed(userIdToDelete);

      
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);

        
        var userInDb = context.Users.Find(userIdToDelete);
        Assert.Null(userInDb);
        Assert.Equal(initialCount - 1, context.Users.Count()); 
    }
}