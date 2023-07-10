using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Domain.DTO;
using TicketsForCinema.Domain.Relations;
using TicketsForCinema.Repository.Interface;
using TicketsForCinema.Service.Interface;

namespace TicketsForCinema.Service.Implementation {
    public class ShoppingCartService : IShoppingCartService {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository, IRepository<EmailMessage> mailRepository) {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
            _mailRepository = mailRepository;
        }


        public bool deleteProductFromSoppingCart(string userId, Guid productId) {
            if (!string.IsNullOrEmpty(userId) && productId != null) {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                _shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId) {
            if (!string.IsNullOrEmpty(userId)) {
                var loggedInUser = _userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                var allProducts = userCard.ProductInShoppingCarts.ToList();

                var allProductPrices = allProducts.Select(z => new {
                    ProductPrice = z.CurrentProduct.ProductPrice,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allProductPrices) {
                    totalPrice += item.Quantity * item.ProductPrice;
                }

                var reuslt = new ShoppingCartDTO {
                    Products = allProducts,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDTO();
        }

        public bool order(string userId) {
            if (!string.IsNullOrEmpty(userId)) {
                var loggedInUser = _userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.Mailto = loggedInUser.Email;
                message.Subject = "Successfuly created order";
                message.Status = false;

                Order order = new Order {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                _orderRepository.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userCard.ProductInShoppingCarts.Select(z => new ProductInOrder {
                    Id = Guid.NewGuid(),
                    ProductId = z.CurrentProduct.Id,
                    Product = z.CurrentProduct,
                    OrderId = order.Id,
                    Order = order
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++) {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Product.ProductPrice;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Product.ProductName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Product.ProductPrice);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                message.Content = sb.ToString();

                productInOrders.AddRange(result);

                foreach (var item in productInOrders) {
                    _productInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                _userRepository.Update(loggedInUser);
                _mailRepository.Insert(message);

                return true;
            }

            return false;
        }
    }
}