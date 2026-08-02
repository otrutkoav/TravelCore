// The DbContext is created by the Admin API host at design time as well as at runtime.
// This keeps database credentials in the API configuration providers (User Secrets locally)
// instead of embedding a design-time connection string in the infrastructure assembly.
