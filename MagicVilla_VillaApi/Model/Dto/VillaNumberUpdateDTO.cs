﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Model.Dto
{
    public class VillaNumberUpdateDTO
    {
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
