//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bakery
{
    using System;
    using System.Collections.Generic;
    
    public partial class GoodsBakery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodsBakery()
        {
            this.Cart = new HashSet<Cart>();
            this.Order = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public string NameGoods { get; set; }
        public int Category { get; set; }
        public Nullable<int> Weight { get; set; }
        public string CallorieValue { get; set; }
        public int Provider { get; set; }
        public int TypeOfGoods { get; set; }
        public int Allergens { get; set; }
        public Nullable<int> OnStock { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Nullable<int> Price { get; set; }
    
        public virtual Allergens Allergens1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        public virtual Provider Provider1 { get; set; }
        public virtual TypeOfGoods TypeOfGoods1 { get; set; }
    }
}
