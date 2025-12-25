using System;

namespace odshop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string Username { get; set; }
        public DateTime OrderDate { get; set; }

        // ÜRÜN İLİŞKİSİ
        public Product Product { get; set; }

        // 🔴 ÖDEME BİLGİLERİ
        public string PaymentType { get; set; }
        public string PaymentInfo { get; set; }
    }
}
