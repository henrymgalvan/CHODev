﻿using cho500.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cho500.Models.ChildRecords
{
    public class ChildBirthFollowUpVisitIndexViewModel  
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgeInWeeks { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public string DateOfFollowup { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Physician { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }

        public int PersonID { get; set; }
    }
}