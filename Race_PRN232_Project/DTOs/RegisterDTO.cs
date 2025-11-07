    namespace Race_PRN232_Project.DTOs
    {
        public class RegisterDTO
        {
            public string FirstName { get; set; } = "";
            public string LastName { get; set; } = "";
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public string ConfirmPassword { get; set; } = "";
            public string Role { get; set; } = ""; // sẽ tự gán trong controller
        }
    }
