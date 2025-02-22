﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Models.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //[ForeignKey("AppUser")]
        public string AppUserId {  get; set; }
        public AppUser User { get; set; }
    }
}
