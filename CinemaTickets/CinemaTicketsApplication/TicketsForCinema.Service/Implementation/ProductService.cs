using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Domain.DTO;
using TicketsForCinema.Domain.Relations;
using TicketsForCinema.Repository.Interface;
using TicketsForCinema.Service.Interface;

namespace TicketsForCinema.Service.Implementation {
    public class ProductService : IProductService {

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<Product> productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository, ILogger<ProductService> logger) {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _logger = logger;
        }

        public bool AddToShoppingCart(AddToShoppingCartDTO item, string userID) {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedProductId != null && userShoppingCard != null) {
                var product = this.GetDetailsForProduct(item.SelectedProductId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (product != null) {
                    ProductInShoppingCart itemToAdd = new ProductInShoppingCart {
                        Id = Guid.NewGuid(),
                        CurrentProduct = product,
                        ProductId = product.Id,
                        UserCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    var existing = userShoppingCard.ProductInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.ProductId == itemToAdd.ProductId).FirstOrDefault();

                    if (existing != null) {
                        existing.Quantity += itemToAdd.Quantity;
                        this._productInShoppingCartRepository.Update(existing);

                    }
                    else {
                        this._productInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewProduct(Product p) {
            _productRepository.Insert(p);
        }

        public void DeleteProduct(Guid id) {
            var product = GetDetailsForProduct(id);
            _productRepository.Delete(product);
        }

        public List<Product> GetAllProducts() {
            _logger.LogInformation("GetAllProducts was called!");
            return _productRepository.GetAll().ToList();
        }

        public Product GetDetailsForProduct(Guid? id) {
            return _productRepository.Get(id);
        }

        public AddToShoppingCartDTO GetShoppingCartInfo(Guid? id) {
            var product = GetDetailsForProduct(id);
            AddToShoppingCartDTO model = new AddToShoppingCartDTO {
                SelectedProduct = product,
                SelectedProductId = product.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingProduct(Product p) {
            _productRepository.Update(p);
        }
    }
}