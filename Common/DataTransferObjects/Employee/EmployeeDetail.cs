namespace Common.DataTransferObjects.ReferenceData
{
    public class EmployeeDetail
    {
        public int EmployeeId { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeEmail { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SupervisorId { get; set; }
        public bool Active { get; set; }
    }
}