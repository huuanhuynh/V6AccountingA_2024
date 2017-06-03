﻿using SummerBreeze;
using SummerBreezeDemo.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SummerBreezeDemo.Models.DTO
{
    [BreezeLocalizable]
    [BreezeAutoGeneratedKeyType(SummerBreezeEnums.AutoGeneratedKeyType.Identity)]
    public class NeedInversePropertiesDTO
    {
        [Key]
        public Guid ObjectId { get; set; }

        [BreezeNavigationProperty("NeedNavigation_ManufacturerDTO", null)] //Association , inverse property and foreign key
        public virtual List<ManufacturerDTO> Manufacturers { get; set; }

        [BreezeNavigationProperty("NeedNavigation_TestDTO", null)] //Association , inverse property and foreign key
        public virtual List<TestDTO> Tests { get; set; }

    }
}