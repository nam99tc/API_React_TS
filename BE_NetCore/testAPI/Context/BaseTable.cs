﻿namespace testAPI.Context
{
    public class BaseTable<T> where T : BaseTable<T>
    {
        public Guid CreatedByUserId { get; set; }
        public Guid LastModifiedByUserId { get; set; }
        public DateTime LastModifiedOnDate { get; set; } = DateTime.Now;
        public DateTime CreatedOnDate { get; set; } = DateTime.Now;
    }
}
