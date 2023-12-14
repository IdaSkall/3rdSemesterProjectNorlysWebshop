﻿namespace WebshopRestService.DTOs
{
    public class PersonDTORead
    {
        public PersonDTORead() { }

        public PersonDTORead(int personId, string? firstName, string? lastName, string? phoneNo, string? email, bool isAdmin)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNo = phoneNo;
            Email = email;
            IsAdmin = isAdmin;
            
        }

        public int PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }   
        }
    }

