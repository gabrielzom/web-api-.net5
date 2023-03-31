using System;

namespace MonitoriaWEBAPI
{
    public class Client
    {
        public int ClientId { get; set; }
        public string NameAndSurname { get; set; }
        public string RegisterOfPhysicalPerson { get; set; }
        public DateTime DateOfBorn { get; set; }
        public string Genre { get; set; }

        public Boolean SomeParameterIsNull()
        {
            return NameAndSurname.Equals("") 
                   || DateOfBorn.Equals(new DateTime()) 
                   || RegisterOfPhysicalPerson.Equals("") 
                   || Genre == null;
        }

    }
}