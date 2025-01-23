namespace Common.DataTransferObjects.ReferenceData
{
    public class EmployeeRoleDetail
    {
        public int EmployeeRoleId { get; set; }
        public int EmployeeId { get; set; }
        public RoleDetail RoleDetail { get; set; }

        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}