using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TicketsForCinema.Domain;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Domain.DTO;
using TicketsForCinema.Domain.Identity;
using TicketsForCinema.Service.Interface;

namespace TicketsForCinema.Web.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase {

        private readonly IOrderService _orderService;
        private readonly UserManager<TicketsForCinemaUser> _userManager;

        public AdminController(IOrderService orderService, UserManager<TicketsForCinemaUser> userManager) {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders() {
            return _orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model) {
            return _orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDTO> model) {
            bool status = true;
            foreach (var item in model) {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null) {
                    var user = new TicketsForCinemaUser {
                        FirstName = item.Name,
                        LastName = item.LastName,
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new ShoppingCart()
                    };
                    var result = _userManager.CreateAsync(user, item.Password).Result;

                    status = status & result.Succeeded;
                }
                else {
                    continue;
                }
            }

            return status;
        }
    }
}
