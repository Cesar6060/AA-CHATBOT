using System.Data;
using System.Text;
using AAU_BE.Database;
using AAU_BE.Models;
using Moq;
using Xunit;

    public class Test
    {
        private LoginService _loginService;
        private UserService _UserService;
        private ToDoService _toDoService;
        private FaqService _faqService;
        private PetService _petService;

        // Constructor to initialize the LoginService and UserService 

        [Fact]
        public void Test_AuthenticateUser_WithInValidCredentials()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";

            // Act
            var user = _loginService.AuthenticateUser(username, password);

            // Assert
            Assert.IsType<LoginService.AuthResult>(_loginService.AuthenticateUser(username, password));
            Assert.False(user.IsAuthenticated);
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

            // Test Case 2
            var tempUserId = _UserService.GetUserId(lr.username);
            // Make sure User Id is returned as int 
            Assert.IsType<int>(tempUserId);

            // Test Case 3
            string tempPass = GenerateRandomPassword(20);
            var changeUserPasswrod = _UserService.UpdateUserPassword(tempUserId, tempPass);
            Assert.IsType<LoginService.AuthResult>(_loginService.AuthenticateUser(lr.username, tempPass));

            // Test Case 4
            var markAccount = _UserService.MarkAccountInactive(tempUserId);
            Assert.True(markAccount, "Temp Account marked Inactive Successfully");

            // Test Case 5
            var userDeleted = _UserService.DeleteUser(tempUserId);
            Assert.True(userDeleted, "User should be deleted successfully.");
        }

        [Fact]
        public void Test_AddFAQ_DeleteFAQ_All_FAQ_Functions()
        {
            FAQ testFaq = new FAQ
            {
                question = "this is a test question",
                id = 1000,
                answer ="This is a test answer",
            };
            //Test Case 1 
            var faqAdded = _faqService.InsertFaq(testFaq);
            Assert.True(faqAdded, "FAQ should be added successfully.");
            
            //Test Case 2
            var faqUpdated = _faqService.UpdateFaQ(testFaq);
            Assert.True(faqAdded, "FAQ should be updated successfully.");
            
            //Test Case 3 
            var faqDeleted = _faqService.DeleteFaq(testFaq.id);
            Assert.True(faqDeleted, "FAQ should be deleted successfully.");
        }
        
        [Fact]
        public void Test_AddPET_DeletePET_All_PET_Functions()
        {
            Pet testPet = new Pet
            {
                userid = "999999",
                age = 10,
                breed = "test",
                name = "name",
                species = "species",
                sex = "sex"
            };
            
            //Test Case 1
            var petAdded = _petService.CreatePet(testPet);
            Assert.True(petAdded, "Pet should be added successfully.");
            
            //Test Case 2
            var petUpdated = _petService.UpdatePet(testPet);
            Assert.True(petUpdated, "Pet should be updated successfully.");
            
            //Test Case3 
            var petDeleted = _petService.DeletePet(testPet.id);
            Assert.True(petDeleted, "Pet should be deleted successfully.");
        }
        
        [Fact]
        public void Test_AddTODO_DeleteTODO_All_TODO_Functions()
        {
            Appointment testAppoint = new Appointment
            {
                UserId = 1,
                Description = "This is a test",
                AppointmentDate = Convert.ToDateTime("2021-09-01 00:00:00"),
                AppointmentTime = Convert.ToDateTime("00:00:00"),
                IsCompleted = true
            };
           //Test Case 1
            var appointAdded = _toDoService.CreateTodo(testAppoint);
            Assert.True(appointAdded, "Appointment should be added successfully.");
            
            //Test Case 2
            int appointId = _toDoService.getTodoID(testAppoint);
            var appointUpdate = _toDoService.UpdateTodo(appointId, testAppoint);
            Assert.True(appointUpdate, "Appointment should be updated successfully.");
            
            //Test Case 3
            var deleteAppoint = _toDoService.DeleteToDo(appointId);
            Assert.True(deleteAppoint, "Appointment should be deleted successfully.");
            
        }

        public static string GenerateRandomPassword(int length)
        {
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
