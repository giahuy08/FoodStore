﻿
namespace FoodStoreAPI.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } 

        public DateTimeOffset CreationDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTimeOffset? ModificationDate { get; set; }

        public int? ModificationBy { get; set; }

        public DateTimeOffset? DeletionDate { get; set; }

        public int? DeleteBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
