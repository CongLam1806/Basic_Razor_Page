using Newtonsoft.Json;
using SaleManagement.BusinessObject.Entity;
using ShoppingCart.BusinessObject.Constants;

namespace SaleManagement.Utilities
{
    public static class SessionHelper
    {
        public static void SetSession<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static bool IsAuthenticated(this ISession session)
        {
            return session.GetSession<Member>(SessionConstant.UserSession) != null;
        }

        public static void SignIn(this ISession session, Member user, bool isAdmin = false)
        {
            session.SetSession(SessionConstant.UserSession, user);
            session.SetSession(SessionConstant.IsAdmin, isAdmin);
        }

        public static void SignOut(this ISession session)
        {
            session.Clear();
        }

        public static T GetCurrentUser<T>(this ISession session)
        {
            return session.GetSession<T>(SessionConstant.UserSession)!;
        }

        public static bool IsAdmin(this ISession session)
        {
            return session.GetSession<bool>(SessionConstant.IsAdmin);
        }

        public static void AddToCart(this ISession session, Product? product)
        {
            var carts = GetSession<HashSet<OrderDetail>>(session, SessionConstant.CartSession) ?? new HashSet<OrderDetail>();
            
            var cart = carts.FirstOrDefault(c => c.ProductId == product?.ProductId);

            if (cart == null)
            {
                cart = new OrderDetail
                {
                    ProductId = product!.ProductId,
                    Product = product!,
                    UnitPrice = product!.UnitPrice,
                    Quantity = 1
                };
                carts.Add(cart);
            } else
            {
                cart.Quantity++;
            }

            session.SetSession(SessionConstant.CartSession, carts);
        }

        public static void DeleteCart(this ISession session, int productId)
        {
            var carts = GetSession<HashSet<OrderDetail>>(session, SessionConstant.CartSession);

            if (carts == null)
            {
                return;
            }

            var cart = carts.FirstOrDefault(c => c.ProductId == productId);

            if (cart == null)
            {
                return;
            }

            carts.Remove(cart);

            session.SetSession(SessionConstant.CartSession, carts);
        }

        public static void DescreaseUnit(this ISession session, int productId)
        {
            var carts = GetSession<HashSet<OrderDetail>>(session, SessionConstant.CartSession);

            if(carts == null)
            {
                return;
            }

            var cart = carts.FirstOrDefault(c => c.ProductId == productId);

            if (cart == null)
            {
                return;
            }

            if (cart.Quantity == 1)
            {
                carts.Remove(cart);
            } else
            {
                cart.Quantity--;
            }

            session.SetSession(SessionConstant.CartSession, carts);
        }

        public static void IncreaseUnit(this ISession session, int productId)
        {
            var carts = GetSession<HashSet<OrderDetail>>(session, SessionConstant.CartSession);

            if (carts == null)
            {
                return;
            }

            var cart = carts.FirstOrDefault(c => c.ProductId == productId);

            if (cart == null)
            {
                return;
            }

            cart.Quantity++;

            session.SetSession(SessionConstant.CartSession, carts);
        }
    }
}
