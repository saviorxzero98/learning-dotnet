using System;
using System.Collections.Generic;

namespace XmlConvertSample.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserProfileDetial Detial { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class UserProfileDetial
    {
        public double Height { get; set; }

        public double Weight { get; set; }

        public List<string> Emails { get; set; }

        public DateTime Birth { get; set; }
    }
}
