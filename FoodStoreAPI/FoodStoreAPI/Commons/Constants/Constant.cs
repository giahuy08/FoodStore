namespace FoodStoreAPI.Commons.Constants
{
    public static class Constants
    {
        public const string NOT_FOUND = "Not Found";
        public const string CANNOT_CREATE = "Cannot create";
        public const string CANNOT_UPDATE = "Cannot update";
        public const string DELETE_SUCCESS = "Delete success";
        public const string DELETE_FAIL = "Delete fail";
        public const string UPDATE_SUCCESS = "Update success";
        public const string UPDATE_FAIL = "Update fail";
        public const string GET_DATA_SUCCESS = "Get data success";
        public const string CREATE_SUCCESS = "Create data success";
        public const string CREATE_FAIL = "Create data fail";

        #region USER
        public const string USER_IS_LOCKED = "User is locked";
        public const string USER_NOT_FOUND = "User not found";
        public const string USER_OR_PASSWORD_ERROR = "User or password not match";
        public const string USER_EXISTED = "User is existed";
        public const string USER_CREATE_SUCCESS = "Create user successfully";
        public const string UNAUTHORIZE = "Unauthorize";
        public const string PASSWORD_WRONG = "Wrong password";
        public const string UPDATE_PASSWORD_SUCCESS = "Update password successfully";
        public const string UPDATE_PASSWORD_FAIL = "Update password fail";
        public const string CONFIRM_EMAIL_ERROR = "Email confirm error";
        public const string CONFIRM_EMAIL_SUCCESS = "Email confirm successfully";
        public const string SEND_EMAIL_SUCCESS = "Send email successfully";
        public const string LOGIN_SUCCESS = "Login success";
        #endregion

        #region CART
        public const string ADD_CART_SUCCESS = "Add to cart success";
        public const string NOT_CART = "Cart wasn't be created";
        #endregion

        #region VALIDATE
        public const string GENDER_ERROR = "Gender is not valid";
        #endregion

        #region CATEGORY
        public const string CATEGORY_EXIST = "Category existed";
        #endregion
    }
}
