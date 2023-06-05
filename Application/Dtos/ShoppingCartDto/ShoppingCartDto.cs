﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ShoppingCartDto
{
    public class ShoppingCartDto
    {
        public decimal TotalCost { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
