using System;

namespace LegacyApp
{
    public interface IClientRepository
    {
        Client GetById(int clientId);
    }

    public interface IUserCreditService : IDisposable
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }

    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
        }

        public UserService()
        {
            var _clientRepository = new ClientRepository();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidUser(firstName, lastName, email, dateOfBirth))
                return false;

            if (_clientRepository == null)
            {
                var _clientRepository = new ClientRepository();
            }

            var client = _clientRepository.GetById(clientId);

            var user = CreateUser(client, firstName, lastName, email, dateOfBirth);

            SetCreditLimit(user);

            if (!IsCreditLimitValid(user))
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsValidUser(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (!email.Contains("@") && !email.Contains("."))
                return false;

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age >= 21;
        }

        private User CreateUser(Client client, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
                user.HasCreditLimit = false;
            else
                user.HasCreditLimit = true;

            return user;
        }

        public void SetCreditLimit(User user)
        {
            if (user.HasCreditLimit)
            {
                int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = user.Client.Type == "ImportantClient" ? creditLimit * 2 : creditLimit;
            }
        }

        public bool IsCreditLimitValid(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
    }
}
