﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
   public class CheckIfConsumerTypeAndDateCreatedExistDTO
    {
        public bool Success { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
