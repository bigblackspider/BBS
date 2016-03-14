namespace BBS.Core.Lib
{
    public static class ErrorMsg
    {
        
        public const string ERR_CUSTOMER_MISSING = "Customer with a ID '{0}' does not exist.";
        public const string ERR_CUSTOMER_EXISTS = "Customer with a ID '{0}' already exists.";
        public const string ERR_CUSTOMER_EMAIL = "Invalid Email address '{0}'.";
        public const string ERR_CUSTOMER_EMAIL_MISSING = "Customer Email address '{0}' does not exist.";
        public const string ERR_CUSTOMER_EMAIL_BLANK = "Customer Email address must have a value.";
        public const string ERR_CUSTOMER_EMAIL_EXISTS = "Customer Email address '{0}' is already registered.";
        public const string ERR_CUSTOMER_MOBILE = "Invalid customer mobile number '{0}'.";
        public const string ERR_CUSTOMER_FAX = "Invalid customer fax number '{0}'.";
        public const string ERR_CUSTOMER_PHONENO = "Invalid customer phone number '{0}'.";
        public const string ERR_CUSTOMER_WEBSITE = "Invalid customer web site URL '{0}'.";
        public const string ERR_CUSTOMER_FIRSTNAME_BLANK = "Customer first name must have a value.";
        public const string ERR_CUSTOMER_FAMILYNAME_BLANK = "Customer family name must have a value.";
        public const string ERR_CUSTOMER_PHONENO_BLANK = "Customer phone number must have a value.";

        public const string ERR_MAILBOX_MISSING = "Mail box '{0}' does not exist.";
        public const string ERR_MAILBOX_DOMAIN = "Invalid mail box domain '{0}'. Must be a valid Url.";
        public const string ERR_MAILBOX_DOMAIN_EXISTS = "Customer mail box domain '{0}' is already registered.";
        public const string ERR_MAILBOX_CATCHALL = "Invalid catch all address'{0}'. Must be a valid Email address.";
        public const string ERR_MAILBOX_EXPIRYDATE = "Invalid expiry date '{0}'.";
        public const string ERR_MAILBOX_ACTIVATE = "Cannot activate mailbox '{0}' as it has expired.";

        public const string ERR_MAILACCOUNT_MAXREACHED = "Cannot create mail account as max accounts for mailbox has been reached.";
        public const string ERR_MAILACCOUNT_ACCOUNTNAME = "Invalid mail account name '{0}'.";
        public const string ERR_MAILACCOUNT_MISSING = "Mail account '{0}' does not exist.";
        public const string ERR_MAILACCOUNT_ACCOUNTNAME_BLANK = "Account name must have a value.";
        public const string ERR_MAILACCOUNT_ACCOUNTNAME_EXISTS = "Account name '{0}' already exists for this mail box.";
        public const string ERR_MAILACCOUNT_FORWARDING = "Invalid mail forward Url '{0}'.";

        public const string ERR_MAILALIAS_MISSING = "Mail alias '{0}' does not exist for this mail domain.";
        public const string ERR_MAILALIAS = "Invalid mail alias '{0}'. Must alphanumeric.";
        public const string ERR_MAILALIAS_EXISTS = "Mail alias '{0}' is already registered for this mail domain.";

        public const string ERR_WEBSITE_MISSING = "Web site '{0}' does not exist.";
        public const string ERR_WEBSITE_DOMAIN = "Invalid web site domain '{0}'. Must be a valid Url.";
        public const string ERR_WEBSITE_DOMAIN_EXISTS = "Web site domain '{0}' is already registered.";
        public const string ERR_WEBSITE_ACTIVATE = "Cannot activate web site '{0}' as it has expired.";

        public const string ERR_WEBSITEITEM_MISSING = "Web site item '{0}' does not exist.";
        public const string ERR_WEBSITEITEM = "Invalid web site item '{0}'. Must be a alphanumeric value.";
        public const string ERR_WEBSITEITEM_EXISTS = "Web site item '{0}' already exists for this web site.";

        public const string ERR_PRODUCT_MISSING = "Product '{0}' does not exist.";
        public const string ERR_PRODUCT = "Invalid product name '{0}'. Must be alphanumeric.";
        public const string ERR_PRODUCT_EXISTS = "Product '{0}' already exists.";

    }
}