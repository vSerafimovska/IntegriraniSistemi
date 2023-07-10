﻿using System;
using System.Collections.Generic;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Domain.DTO;

namespace TicketsForCinema.Service.Interface {
    public interface IProductService {

        List<Product> GetAllProducts();

        Product GetDetailsForProduct(Guid? id);

        void CreateNewProduct(Product p);

        void UpdeteExistingProduct(Product p);

        AddToShoppingCartDTO GetShoppingCartInfo(Guid? id);

        void DeleteProduct(Guid id);

        bool AddToShoppingCart(AddToShoppingCartDTO item, string userID);

    }
}