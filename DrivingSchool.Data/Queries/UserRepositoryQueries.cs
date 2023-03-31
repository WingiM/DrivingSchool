namespace DrivingSchool.Data.Queries;

public static class UserRepositoryQueries
{
    public const string GetUserAvatar = @"SELECT avatar FROM public.user WHERE id = @id";
    public const string IsUserDeleted = "SELECT is_deleted FROM public.user WHERE id = @id";

    public const string GetUserDefaultAvatar = @"SELECT claim_value 
                                                  FROM blazor_identity.user_claim 
                                                  WHERE user_id = 
                                                        (SELECT identity_id FROM public.user WHERE id=@id) 
                                                    AND claim_type = @claimType";

    public const string UpdateUserAvatar = @"UPDATE public.user SET avatar = @fileName WHERE id = @id";

    public const string DeleteUserAvatar = "UPDATE public.user SET avatar = null WHERE id = @id";
    public const string DeleteUser = "UPDATE public.user SET is_deleted = true WHERE id = @id";
}