﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Order { get; set; }
    }
}