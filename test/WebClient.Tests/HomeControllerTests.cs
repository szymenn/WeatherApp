using Microsoft.AspNetCore.Mvc;
using WebClient.Controllers;
using Xunit;

namespace WebClient.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_Always_ReturnsViewResult()
        {
            var homeController = new HomeController();

            var actual = homeController.Index();

            Assert.IsType<ViewResult>(actual);
        }

    }
}