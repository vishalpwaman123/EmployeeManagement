using BusinessModel.Interface;

namespace EmployeeManagementTestCase
{
    internal class userController
    {
        private IUserBL business;

        public userController(IUserBL business)
        {
            this.business = business;
        }
    }
}