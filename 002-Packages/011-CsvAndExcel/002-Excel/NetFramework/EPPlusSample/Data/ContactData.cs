using System;
using System.Collections.Generic;

namespace EPPlusSample.Data
{
    public class ContactData
    {
        public static List<Contact> ContactList
        {
            get
            {
                return new List<Contact>()
                {
                    new Contact() { ID = 0, Name="Ram", Email="ram@techbrij.com", Phone="111-222-3333", CreateDate = DateTime.Now.AddYears(-2) },
                    new Contact() { ID = 1, Name="Shyam", Email="shyam@techbrij.com", Phone="159-222-1596", CreateDate = DateTime.Now.AddYears(-1) },
                    new Contact() { ID = 2, Name="Mohan", Email="mohan@techbrij.com", Phone="456-222-4569", CreateDate = DateTime.Now.AddMonths(-6) },
                    new Contact() { ID = 3, Name="Sohan", Email="sohan@techbrij.com", Phone="789-456-3333", CreateDate = DateTime.Now.AddDays(-7) },
                    new Contact() { ID = 4, Name="Karan", Email="karan@techbrij.com", Phone="111-222-1234", CreateDate = DateTime.Now.AddHours(-8) },
                    new Contact() { ID = 5, Name="Brij", Email="brij@techbrij.com", Phone="111-222-3333", CreateDate = DateTime.Now }
                };
            }
        } 
    }

    public class Contact
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
