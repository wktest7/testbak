﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public interface IOrderService
    {
        Task AddAsync(OrderCreateDto orderCreateDto);
    }
}