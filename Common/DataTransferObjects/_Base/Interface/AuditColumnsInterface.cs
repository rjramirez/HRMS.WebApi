﻿namespace Common.DataTransferObjects._Base.Interface
{
    public interface AuditColumnsInterface
    {
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
