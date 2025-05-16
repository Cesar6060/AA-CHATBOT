using System.Data;
using System.Text;
using AAU_BE.Database;
using AAU_BE.Models;
using Moq;
using Xunit;

namespace AAU_BE.Unit_Tests
{
    public class Test
    {
        private LoginService _loginService;
        private UserService _UserService;

        // Constructor to initialize the LoginService and UserService 
        public Test()
        {
            // Mocking the services if necessary
            _loginService = new LoginService(); 
            _UserService = new UserService();
        }

        [Fact]
        public void Test_AuthenticateUser_WithValidCredentials()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";
            
            Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

            
            Assert.True(File.Exists("../../Database/AAU.DB\""), "Database file should exist.");
            

            // Act
            var user = _loginService.AuthenticateUser(username, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(username, user.UserLevel);
        }

        [Fact]
        public void Test_AddUser_DeleteUser_All_User_Functions()
        {
            // Arrange
            LoginRequest lr = new LoginRequest
            {
                username = "testy1231154",
                password = "aldfjaof[ja;df",
                email = "test@test.com" 
            };

            // Test Case 1
            var userAdded = _loginService.AddUser(lr);
            // Assert test case  
            Assert.True(userAdded, "User should be added successfully.");
            Console.WriteLine("Test Case 1 PASSED.");

            // Test Case 2
            var tempUserId = _UserService.GetUserId(lr.username);
            // Make sure User Id is returned as int 
            Assert.IsType<int>(tempUserId);
            Console.WriteLine("Test Case 2 PASSED.");

            // Test Case 3
            string tempPass = GenerateRandomPassword(20);
            var changeUserPasswrod = _UserService.UpdateUserPassword(tempUserId, tempPass);
            Assert.IsType<LoginService.AuthResult>(_loginService.AuthenticateUser(lr.username, tempPass));
            Console.WriteLine("Test Case 3 PASSED.");

            // Test Case 4
            var markAccount = _UserService.MarkAccountInactive(tempUserId);
            Assert.True(markAccount, "Temp Account marked Inactive Successfully");
            Console.WriteLine("Test Case 4 PASSED.");

            // Test Case 5
            var userDeleted = _UserService.DeleteUser(tempUserId);
            Assert.True(userDeleted, "User should be deleted successfully.");
            Console.WriteLine("Test Case 5 PASSED.");
            
            Console.WriteLine("ALL USER TESTS PASSED");
        }

        public static string GenerateRandomPassword(int length)
        {
            // Define the character set to choose from
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();

            // Generate random characters
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                passwordBuilder.Append(validChars[index]);
            }

            return passwordBuilder.ToString();
        }
    }
}
