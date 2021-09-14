namespace Parkeringshuset.Models
{
    public class Vehicle
    {
        private string RegistrationNumber = null;

        public Vehicle(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;

        }

        public override string ToString()
        {
            return RegistrationNumber;
        }
    }
}
